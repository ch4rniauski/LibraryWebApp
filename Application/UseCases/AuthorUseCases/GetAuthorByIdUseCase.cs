using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthorUseCases;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.AuthorUseCases
{
    public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAuthorByIdUseCase(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CreateAuthorRecord> Execute(Guid id)
        {
            var author = await _uow.AuthorRepository.GetById(id);

            if (author is null)
                throw new NotFoundException("Author with that ID doesn't exist");

            return _mapper.Map<CreateAuthorRecord>(author);
        }
    }
}
