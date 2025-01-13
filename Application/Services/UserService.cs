using Application.Abstractions.Services;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Abstractions.UnitsOfWork;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task BorrowBook(Guid userId, Guid bookId)
        {
            var user = await _uow.UserRepository.GetById(userId);

            if (user is null)
                throw new NotFoundException("User with that ID doesn't exist");

            var book = await _uow.BookRepository.GetById(bookId);

            if (book is null)
                throw new NotFoundException("Book with that ID doesn't exist");
            if (book.UserId is not null)
                throw new IncorrectDataException("That book is already borrowed");

            book.TakenAt = DateOnly.FromDateTime(DateTime.UtcNow);
            book.DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30));
            book.UserId = user.Id;
            book.User = user;

            user.Books.Add(book);

            await _uow.Save();
        }

        public async Task<UserInfoResponse> GetUserInfo(Guid id)
        {
            var user = await _uow.UserRepository.GetById(id);

            if (user is null)
                throw new NotFoundException("User with that ID doesn't exist");

            return _mapper.Map<UserInfoResponse>(user);
        }
    }
}
