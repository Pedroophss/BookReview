using MyRead.Domain.Entities;
using MyRead.Domain.Resources;
using Shouldly;
using System.Linq;
using Xunit;

namespace MyRead.UnitTests.Cases.Domain.v2
{
    [Trait("Domain", "Entities V2")]
    public class BookEntityTests
    {
        [Fact(DisplayName = "BookEntity: When invalid title")]
        public void Book_InvalidTitle_ShouldHaveNameValidation()
        {
            // Assert
            var book = new BookBuilder().WithEmptyTitle().Build();

            // Act
            var validations = book.Validate();

            // Assert
            validations.ShouldHaveSingleItem();
            validations.Single().Source.ShouldBe(nameof(BookEntity));
            validations.Single().MessageKey.ShouldBe(nameof(DomainStrs.BookNameRequired));
        }

        [Fact(DisplayName = "BookEntity: When invalid author")]
        public void Book_InvalidAuthor_ShouldHaveAuthorValidation()
        {
            // Assert
            var book = new BookBuilder().WithEmptyAuthors().Build();

            // Act
            var validations = book.Validate();

            // Assert
            validations.ShouldHaveSingleItem();
            validations.Single().Source.ShouldBe(nameof(BookEntity));
            validations.Single().MessageKey.ShouldBe(nameof(DomainStrs.BookAuthorRequired));
        }

        [Fact(DisplayName = "BookEntity: When zero pages")]
        public void Book_ZeroPages_ShouldHavePagesValidation()
        {
            // Assert
            var book = new BookBuilder().WithZeroPages().Build();

            // Act
            var validations = book.Validate();

            // Assert
            validations.ShouldHaveSingleItem();
            validations.Single().Source.ShouldBe(nameof(BookEntity));
            validations.Single().MessageKey.ShouldBe(nameof(DomainStrs.BookPagesRequired));
        }
    }
}