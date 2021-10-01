using Bogus;

namespace MyRead.UnitTests.Cases.Domain.v3.Entities.Book
{
    public class ZeroPagesBook : ValidBook
    {
        public ZeroPagesBook(Faker bogus)
            : base(bogus, _pages: 0) { }
    }
}