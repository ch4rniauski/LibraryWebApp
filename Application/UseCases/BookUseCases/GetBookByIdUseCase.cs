using Application.Abstractions.Records;
using Application.Abstractions.UseCases.BookUseCases;
using Application.Exceptions.CustomExceptions;
using AutoMapper;
using Domain.Abstractions.UnitsOfWork;

namespace Application.UseCases.BookUseCases
{
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetBookByIdUseCase(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GetBookRecord> Execute(Guid id)
        {
            var book = await _uow.BookRepository.GetById(id);

            if (book is null)
                throw new NotFoundException("Book with that ID wasn't found");

            return _mapper.Map<GetBookRecord>(book);
        }
    }
}
