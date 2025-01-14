using Application.Abstractions.Records;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles.BookProfiles
{
    public class BookEntityToGetBookResponse : Profile
    {
        public BookEntityToGetBookResponse()
        {
            CreateMap<BookEntity, GetBookResponse>()
                .ConstructUsing(src => new GetBookResponse(
                    src.Id,
                    src.Title,
                    src.ImageData));
        }
    }
}
