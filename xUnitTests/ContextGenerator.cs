using Library.DataContext;
using Microsoft.EntityFrameworkCore;

namespace xUnitTests
{
    public static class ContextGenerator
    {
        static public LibraryContext Generate()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("LibraryDb")
                .Options;

            return new LibraryContext(options);
        }
    }
}
