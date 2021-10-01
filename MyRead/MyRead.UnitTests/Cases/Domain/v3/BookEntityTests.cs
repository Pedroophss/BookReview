using MyRead.Domain.Entities;
using MyRead.Domain.Resources;
using MyRead.UnitTests.Cases.Domain.v3.Entities.Book;
using MyRead.UnitTests.Core;
using Shouldly;
using System.Linq;
using Xunit;

namespace MyRead.UnitTests.Cases.Domain.v3
{
    [Trait("Domain", "Entities V3")]
    public class BookEntityTests
    {
        [Theory(DisplayName = "BookEntity: When invalid title"), AutoAssert]
        public void Book_InvalidTitle_ShouldHaveNameValidation(EmptyTitleBook book)
        {
            // Act
            var validations = book.Object.Validate();

            // Assert
            validations.ShouldHaveSingleItem();
            validations.Single().Source.ShouldBe(nameof(BookEntity));
            validations.Single().MessageKey.ShouldBe(nameof(DomainStrs.BookNameRequired));
        }

        [Theory(DisplayName = "BookEntity: When invalid author"), AutoAssert]
        public void Book_InvalidAuthor_ShouldHaveAuthorValidation(EmptyAuthorsBook book)
        {
            // Act
            var validations = book.Object.Validate();

            // Assert
            validations.ShouldHaveSingleItem();
            validations.Single().Source.ShouldBe(nameof(BookEntity));
            validations.Single().MessageKey.ShouldBe(nameof(DomainStrs.BookAuthorRequired));
        }

        [Theory(DisplayName = "BookEntity: When zero pages"), AutoAssert]
        public void Book_ZeroPages_ShouldHavePagesValidation(ZeroPagesBook book)
        {
            // Act
            var validations = book.Object.Validate();

            // Assert
            validations.ShouldHaveSingleItem();
            validations.Single().Source.ShouldBe(nameof(BookEntity));
            validations.Single().MessageKey.ShouldBe(nameof(DomainStrs.BookPagesRequired));
        }
    }
}