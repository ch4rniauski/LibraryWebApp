using Application.Abstractions.Records;
using Application.Abstractions.UseCases.BookUseCases;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.BookUseCases
{
    public class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllBooksUseCase(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<GetBookRecord>?> Execute()
        {
            var list = await _uow.BookRepository.GetAll();

            var booksToReturn = _mapper.Map<List<GetBookRecord>>(list);

            return booksToReturn;
        }
    }
}
