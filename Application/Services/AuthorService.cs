using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Abstractions.UseCases.AuthorUseCases;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ICreateAuthorUseCase _createAuthorUseCase;
        private readonly IDeleteAutorUseCase _deleteAuthorUseCase;
        private readonly IGetAllAuthorsUseCase _getAllAuthorsUseCase;
        private readonly IGetAuthorByIdUseCase _getAuthorByIdUseCase;
        private readonly IUpdateAuthorUseCase _updateAuthorUseCase;

        public AuthorService(ICreateAuthorUseCase createAuthorUseCase,
            IDeleteAutorUseCase deleteAuthorUseCase,
            IGetAllAuthorsUseCase getAllAuthorsUseCase,
            IGetAuthorByIdUseCase getAuthorByIdUseCase,
            IUpdateAuthorUseCase updateAuthorUseCase)
        {
            _createAuthorUseCase = createAuthorUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _getAllAuthorsUseCase = getAllAuthorsUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _updateAuthorUseCase = updateAuthorUseCase;
        }

        public async Task CreateAuthor(CreateAuthorRecord author)
        {
            await _createAuthorUseCase.Execute(author);
        }

        public async Task DeleteAutor(Guid id)
        {
            await _deleteAuthorUseCase.Execute(id);
        }

        public async Task<List<CreateAuthorRecord>?> GetAllAuthors()
        {
            var list = await _getAllAuthorsUseCase.Execute();

            return list;
        }

        public async Task<CreateAuthorRecord> GetAuthorById(Guid id)
        {
            var author = await _getAuthorByIdUseCase.Execute(id);

            return author;
        }

        public async Task UpdateAuthor(UpdateAuthorRecord author)
        {            
            await _updateAuthorUseCase.Execute(author);
        }
    }
}
