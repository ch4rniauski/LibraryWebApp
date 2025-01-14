using Application.Abstractions.Records;
using Application.Abstractions.UseCases.UserUseCases;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.UserUseCases
{
    public class GetUserInfoUseCase : IGetUserInfoUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetUserInfoUseCase(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<UserInfoResponse> Execute(Guid id)
        {
            var user = await _uow.UserRepository.GetById(id);

            if (user is null)
                throw new NotFoundException("User with that ID doesn't exist");

            return _mapper.Map<UserInfoResponse>(user);
        }
    }
}
