using Bogus;

namespace MyRead.UnitTests.Cases.Domain.v3.Entities.Book
{
    public class EmptyTitleBook : ValidBook
    {
        public EmptyTitleBook(Faker bogus)
            : base(bogus, _title: string.Empty) { }
    }
}