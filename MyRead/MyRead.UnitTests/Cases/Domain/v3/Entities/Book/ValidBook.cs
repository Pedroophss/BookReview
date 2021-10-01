using Bogus;
using MyRead.Domain.Entities;
using MyRead.UnitTests.Core;

namespace MyRead.UnitTests.Cases.Domain.v3.Entities.Book
{
    public class ValidBook : RuleModel<BookEntity>
    {
        // Construtor usado pelo autofixture:
        public ValidBook(Faker bogus)
            : this(bogus, _title: null) { }

        // Construtor usado por outros estados de user
        protected ValidBook(Faker bogus, string _title = null, string? _authors = null, int? _pages = null, string _language = null)
            : base(new BookEntity
            (
                title: _title ?? bogus.Lorem.Sentence(3),
                authors: _authors ?? bogus.Name.FullName(),
                language: _language ?? bogus.Random.String(3),
                pages: _pages ?? bogus.Random.Int(0)
            ))
        { }
    }
}