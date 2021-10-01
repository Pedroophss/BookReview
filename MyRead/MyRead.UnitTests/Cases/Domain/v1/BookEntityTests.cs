using MyRead.Domain.Entities;
using MyRead.Domain.Resources;
using Shouldly;
using System.Linq;
using Xunit;

namespace MyRead.UnitTests.Cases.Domain.v1
{
    [Trait("Domain", "Entities")]
    public class BookEntityTests
    {
        [Fact(DisplayName = "BookEntity: When invalid title")]
        public void Book_InvalidTitle_ShouldHaveNameValidation()
        {
            // Assert
            var book = new BookEntity(string.Empty, "me", "eng", 100);

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
            var book = new BookEntity("name", string.Empty, "eng", 100);

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
            var book = new BookEntity("name", "me", "eng", 0);

            // Act
            var validations = book.Validate();

            // Assert
            validations.ShouldHaveSingleItem();
            validations.Single().Source.ShouldBe(nameof(BookEntity));
            validations.Single().MessageKey.ShouldBe(nameof(DomainStrs.BookPagesRequired));
        }
    }
}