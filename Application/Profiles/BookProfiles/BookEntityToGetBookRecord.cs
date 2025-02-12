using Application.Abstractions.Records;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles.BookProfiles
{
    public class BookEntityToGetBookRecord : Profile
    {
        public BookEntityToGetBookRecord()
        {
            CreateMap<BookEntity, GetBookRecord>()
                .ConstructUsing(src => new GetBookRecord(
                    src.Id,
                    src.ISBN,
                    src.Title,
                    src.Genre,
                    src.Description,
                    src.ImageData,
                    src.TakenAt,
                    src.DueDate,
                    src.UserId));
        }
    }
}
