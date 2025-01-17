using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.BookUseCases;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.BookUseCases
{
    public class GetBooksWithParamsUseCase : IGetBooksWithParamsUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetBooksWithParamsUseCase(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<GetBookResponse>?> Execute(GetBookRequest request)
        {
            var books = request.SortBy switch
            {
                "genre" => await _uow.BookRepository.SortByGenreAndSearch(request.Search),
                "author" => await _uow.BookRepository.SortByAuthorAndSearch(request.Search),
                _ => await _uow.BookRepository.GetAll()
            };

            return _mapper.Map<List<GetBookResponse>>(books);
        }
    }
}
