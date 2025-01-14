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
            List<GetBookResponse>? booksToReturn = new();
            var books = await _uow.BookRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Search) && books is not null)
            {
                books = books
                    .Where(b => b.Title.Contains(request.Search))
                    .ToList();
            }

            if (books is not null)
            {
                books = request.SortBy switch
                {
                    "genre" => books.OrderBy(b => b.Genre).ToList(),
                    "author" => books.OrderBy(b => b.AuthorFirstName).ThenBy(b => b.AuthorSecondName).ToList(),
                    _ => books
                };
            }

            if (books is not null)
                booksToReturn = _mapper.Map<List<GetBookResponse>>(books);

            return booksToReturn;
        }
    }
}
