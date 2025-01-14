using Application.Abstractions.Records;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.BookUseCases
{
    public class GetBookByISBNUseCase : IGetBookByISBNUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetBookByISBNUseCase(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GetBookRecord> Execute(string ISBN)
        {
            var book = await _uow.BookRepository.GetBookByISBN(ISBN);

            if (book is null)
                throw new NotFoundException("Book with that ISBN wasn't found");

            return _mapper.Map<GetBookRecord>(book);
        }
    }
}
