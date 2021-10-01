using MyRead.Bind.Models;
using MyRead.Domain.Entities;
using System.Collections.Generic;
using Xunit;

namespace MyRead.UnitTests.Cases.Bind.v2
{
    [Trait("Bind", "Models V2")]
    public class BookItemTests
    {
        private const byte Rating = 5;
        private const string Review = "legal :)";

        private UserEntity User { get; }

        public BookItemTests() =>
            User = new UserEntity(string.Empty, 18, true);

        [Fact(DisplayName = "BookItemBuilder: When valid input")]
        public void BookItemBuilder_ValidCsvRow_ShouldGenerateItem()
        {
            // Arrange
            var builder = new BookItemBuilder(i => Review, () => Rating, new List<UserEntity> { User });
            var rowCsv = "1,Harry Potter and the Half-Blood Prince (Harry Potter  #6),J.K. Rowling/Mary GrandPrÃ©,4.57,0439785960,9780439785969,eng,652,2,3,9/16/2006,Scholastic Inc.";

            // Act
            var item = builder.CreateOne(rowCsv);

            // Assert
            AssertBook(item.Book);
            AssertCounts(item.Count);
            AssertRatings(item.Ratings);
            AssertReviwers(item.Reviews);
        }

        private void AssertBook(BookEntity book)
        {
            Assert.Equal(652, book.Pages);
            Assert.Equal("eng", book.Language);
            Assert.Equal("J.K. Rowling/Mary GrandPrÃ©", book.Authors);
            Assert.Equal("Harry Potter and the Half-Blood Prince (Harry Potter  #6)", book.Title);
        }

        private void AssertRatings(List<BookRatingEntity> ratings)
        {
            Assert.Equal(2, ratings.Count);

            AssertRating(ratings[0]);
            AssertRating(ratings[1]);
        }

        private void AssertRating(BookRatingEntity rating)
        {
            Assert.Equal(Rating, rating.Rating);
            Assert.Equal(User.Id, rating.UserId);
        }

        private void AssertReviwers(List<BookReviewEntity> reviews)
        {
            Assert.Equal(3, reviews.Count);

            AssertReview(reviews[0]);
            AssertReview(reviews[1]);
            AssertReview(reviews[2]);
        }

        private void AssertReview(BookReviewEntity review)
        {
            Assert.Equal(Review, review.Reviwer);
            Assert.Equal(User.Id, review.UserId);
        }

        private void AssertCounts(BookCountsEntity count)
        {
            Assert.Equal((uint)2, count.Ratings);
            Assert.Equal((uint)3, count.Reviwers);
        }
    }
}