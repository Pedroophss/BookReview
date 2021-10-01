using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRead.Domain.Entities;

namespace MyRead.Infra.Entity.Mappings
{
    internal class BookCountsMap : IEntityTypeConfiguration<BookCountsEntity>
    {
        public void Configure(EntityTypeBuilder<BookCountsEntity> builder)
        {
            builder.ToTable("book");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("id");

            builder.Property(x => x.Ratings)
                   .HasColumnName("ratings_count");

            builder.Property(x => x.Reviwers)
                   .HasColumnName("text_reviews_count");

            builder.Property(x => x.TotalRating)
                   .HasColumnName("total_rating");
        }
    }
}