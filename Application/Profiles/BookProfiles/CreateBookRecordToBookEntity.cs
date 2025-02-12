using Application.Abstractions.Requests;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles.BookProfiles
{
    public class CreateBookRecordToBookEntity : Profile
    {
        public CreateBookRecordToBookEntity()
        {
            CreateMap<CreateBookRecord, BookEntity>()
                .ForMember(dist => dist.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(dist => dist.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dist => dist.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dist => dist.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dist => dist.TakenAt, opt => opt.MapFrom(src => src.TakenAt))
                .ForMember(dist => dist.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dist => dist.ImageData, opt => opt.MapFrom(src => src.ImageData));
        }
    }
}
