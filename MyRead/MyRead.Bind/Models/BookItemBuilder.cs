using MyRead.Bind.Delegates;
using MyRead.Bind.ValueObjects;
using MyRead.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRead.Bind.Models
{
    public class BookItemBuilder
    {
        const int MaxGenerates = 10_000;

        public BookItemBuilder(GenerateWords words, GenerateRating ratings, List<UserEntity> users)
        {
            Words = words;
            Users = users;
            Ratings = ratings;
            Randomizer = new Random(DateTime.Now.Millisecond);
        }

        Random Randomizer { get; }
        GenerateWords Words { get; }
        GenerateRating Ratings { get; }
        List<UserEntity> Users { get; }

        public BookItem CreateOne(string csvRow)
        {
            var row = new CsvRow(csvRow);
            var book = CreateBook(row);

            BookItem item = new()
            {
                Book = book,
                Ratings = GenerateRatings(book.Id, row.GetColumn<int>(8)).ToList(),
                Reviews = GenerateReviwes(book.Id, row.GetColumn<int>(9)).ToList(),
            };

            item.CalculateCounts();
            return item;
        }

        private UserEntity GetUser()
        {
            var randomUser = Randomizer.Next(0, Users.Count);
            return Users[randomUser];
        }

        private static BookEntity CreateBook(CsvRow row) =>
            new(row.GetColumn<string>(1), row.GetColumn<string>(2), row.GetColumn<string>(6), row.GetColumn<int>(7));

        private IEnumerable<BookRatingEntity> GenerateRatings(Guid bookId, int numberOfRating)
        {
            var howMany = Math.Min(numberOfRating, MaxGenerates);
            for (int i = 0; i < howMany; i++)
            {
                var user = GetUser();
                yield return new(user.Id, bookId, Ratings());
            }
        }

        private IEnumerable<BookReviewEntity> GenerateReviwes(Guid bookId, int numberOfReviews)
        {
            var howMany = Math.Min(numberOfReviews, MaxGenerates);
            for (int i = 0; i < howMany; i++)
            {
                var user = GetUser();
                var sentences = Words(Randomizer.Next(0, 1000));

                yield return new(user.Id, bookId, sentences);
            }
        }
    }
}
