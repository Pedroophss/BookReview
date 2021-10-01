using MyRead.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRead.Bind.Models
{
    public sealed class BookItem
    {
        public BookEntity Book { get; init; }
        public BookCountsEntity Count { get; private set; }
        public List<BookRatingEntity> Ratings { get; init; }
        public List<BookReviewEntity> Reviews { get; init; }

        public void CalculateCounts()
        {
            Count = new
            (
                id: Guid.NewGuid(),
                ratings: (uint)Ratings.Count,
                reviwers: (uint)Reviews.Count,
                totalRating: (uint)Ratings.Sum(s => s.Rating)
            );
        }
    }
}