using MyRead.Domain.Models;
using MyRead.Domain.Resources;
using System;
using System.Collections.Generic;

namespace MyRead.Domain.Entities
{
    public class BookReviewEntity : Entity
    {
        public BookReviewEntity(Guid userId, Guid bookId, string reviwer)
            : this(Guid.NewGuid(), userId, bookId, reviwer) { }

        public BookReviewEntity(Guid id, Guid userId, Guid bookId, string reviwer)
            : base(id)
        {
            UserId = userId;
            BookId = bookId;
            Reviwer = reviwer;
        }

        public Guid UserId { get; }
        public Guid BookId { get; }
        public string Reviwer { get; }

        public override IEnumerable<ValidationItem> Validate()
        {
            if (string.IsNullOrWhiteSpace(Reviwer))
                yield return new(nameof(BookReviewEntity), nameof(DomainStrs.BookReviwerTextRequired));
        }
    }
}