using Application.Abstractions.Records;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.BookUseCases
{
    public class GetBooksByUserIdUseCase : IGetBooksByUserIdUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetBooksByUserIdUseCase(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<GetBookRecord>?> Execute(Guid id)
        {
            var user = await _uow.UserRepository.GetById(id);

            if (user is null)
                throw new NotFoundException("User with that ID doesn't exist");

            var books = await _uow.BookRepository.GetBooksByUserId(id);

            var booksToReturn = _mapper.Map<List<GetBookRecord>>(books);

            return booksToReturn;
        }
    }
}
