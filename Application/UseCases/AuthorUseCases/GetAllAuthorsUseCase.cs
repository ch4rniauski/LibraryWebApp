using Application.Abstractions.Requests;
using Application.Abstractions.UseCases.AuthorUseCases;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.AuthorUseCases
{
    public class GetAllAuthorsUseCase : IGetAllAuthorsUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllAuthorsUseCase(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<CreateAuthorRecord>?> Execute()
        {
            var list = await _uow.AuthorRepository.GetAll();

            if (list is null)
                return null;

            var listToReturn = _mapper.Map<List<CreateAuthorRecord>>(list);

            return listToReturn;
        }
    }
}
