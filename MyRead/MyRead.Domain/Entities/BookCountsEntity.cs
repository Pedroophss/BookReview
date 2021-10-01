using MyRead.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRead.Domain.Entities
{
    public class BookCountsEntity : Entity
    {
        public BookCountsEntity(Guid id, uint ratings, uint reviwers, uint totalRating)
            : base(id)
        {
            Ratings = ratings;
            Reviwers = reviwers;
            TotalRating = totalRating;
        }

        public uint Ratings { get; private set; }
        public uint Reviwers { get; private set; }
        public uint TotalRating { get; private set; }

        public override IEnumerable<ValidationItem> Validate() =>
            Enumerable.Empty<ValidationItem>();

        public void NewRating(uint rating)
        {
            Ratings++;
            TotalRating += rating;
        }

        public void NewReviwer() =>
            Reviwers++;
    }
}