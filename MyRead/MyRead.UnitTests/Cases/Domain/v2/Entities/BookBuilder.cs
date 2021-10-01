using Bogus;
using MyRead.Domain.Entities;

namespace MyRead.UnitTests.Cases.Domain.v2
{
    public class BookBuilder
    {
        public int Pages { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Language { get; set; }

        public BookBuilder()
        {
            var bogus = new Faker();

            Pages = bogus.Random.Int(0);
            Title = bogus.Lorem.Sentence(3);
            Authors = bogus.Lorem.Sentence(3);
            Language = bogus.Random.String(3);
        }

        public BookBuilder WithZeroPages()
        {
            Pages = 0;
            return this;
        }

        public BookBuilder WithEmptyTitle()
        {
            Title = string.Empty;
            return this;
        }

        public BookBuilder WithEmptyAuthors()
        {
            Authors = string.Empty;
            return this;
        }

        public BookEntity Build() =>
            new(Title, Authors, Language, Pages);
    }
}