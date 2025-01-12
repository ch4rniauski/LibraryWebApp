using AutoMapper;
using Domain.Abstractions.Records;
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
                    src.AuthorFirstName,
                    src.AuthorSecondName,
                    src.ImageData,
                    src.TakenAt,
                    src.DueDate,
                    src.UserId));
        }
    }
}
