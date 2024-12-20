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
                .ForMember(dist => dist.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dist => dist.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dist => dist.ImageURL, opt => opt.MapFrom(src => src.ImageURL));

            CreateMap<BookEntity, GetBookRecord>()
                .ForMember(dist => dist.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(dist => dist.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dist => dist.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
                .ForMember(dist => dist.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dist => dist.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dist => dist.AuthorFirstName, opt => opt.MapFrom(src => src.AuthorFirstName))
                .ForMember(dist => dist.AuthorSecondName, opt => opt.MapFrom(src => src.AuthorSecondName))
                .ForMember(dist => dist.TakenAt, opt => opt.MapFrom(src => src.TakenAt))
                .ForMember(dist => dist.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dist => dist.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dist => dist.UserId, opt => opt.MapFrom(src => src.User));

            CreateMap<CreateBookRecord, BookEntity>()
                .ForMember(dist => dist.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(dist => dist.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dist => dist.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
                .ForMember(dist => dist.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dist => dist.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dist => dist.AuthorFirstName, opt => opt.MapFrom(src => src.AuthorFirstName))
                .ForMember(dist => dist.AuthorSecondName, opt => opt.MapFrom(src => src.AuthorSecondName))
                .ForMember(dist => dist.TakenAt, opt => opt.MapFrom(src => src.TakenAt))
                .ForMember(dist => dist.DueDate, opt => opt.MapFrom(src => src.DueDate));
        }
    }
}
