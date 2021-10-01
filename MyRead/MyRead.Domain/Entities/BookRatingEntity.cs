using MyRead.Domain.Models;
using MyRead.Domain.Resources;
using System;
using System.Collections.Generic;

namespace MyRead.Domain.Entities
{
    public class BookRatingEntity : Entity
    {
        public BookRatingEntity(Guid userId, Guid bookId, byte rating)
            : this(Guid.NewGuid(), userId, bookId, rating) { }

        public BookRatingEntity(Guid id, Guid userId, Guid bookId, byte rating)
            : base(id)
        {
            UserId = userId;
            BookId = bookId;
            Rating = rating;
        }

        public Guid UserId { get; }
        public Guid BookId { get; }
        public byte Rating { get; }

        public override IEnumerable<ValidationItem> Validate()
        {
            if (Rating > 5)
                yield return new(nameof(BookRatingEntity), nameof(DomainStrs.BookRatingInvalidNumber));
        }
    }
}