using Bogus;

namespace MyRead.UnitTests.Cases.Domain.v3.Entities.Book
{
    public class EmptyAuthorsBook : ValidBook
    {
        public EmptyAuthorsBook(Faker bogus)
            : base(bogus, _authors: string.Empty) { }
    }
}