using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Entities;

namespace Domain.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookEntity, GetBookResponse>()
                .ConstructUsing(src => new GetBookResponse(
                    src.Id,
                    src.Title, 
                    src.ImageData));

            CreateMap<BookEntity, GetBookRecord>()
                .ConstructUsing(src => new GetBookRecord(
                    src.Id,
                    src.ISBN,
                    src.Title,
                    src.Genre,
                    src.Description,
                    src.AuthorFirstName,
                    src.AuthorSecondName,
                    src.ImageData,
                    src.TakenAt,
                    src.DueDate,
                    src.UserId));

            CreateMap<CreateBookRecord, BookEntity>()
                .ForMember(dist => dist.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(dist => dist.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dist => dist.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dist => dist.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dist => dist.AuthorFirstName, opt => opt.MapFrom(src => src.AuthorFirstName))
                .ForMember(dist => dist.AuthorSecondName, opt => opt.MapFrom(src => src.AuthorSecondName))
                .ForMember(dist => dist.TakenAt, opt => opt.MapFrom(src => src.TakenAt))
                .ForMember(dist => dist.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dist => dist.ImageData, opt => opt.MapFrom(src => src.ImageData));
        }
    }
}
