using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRead.Domain.Entities;

namespace MyRead.Infra.Entity.Mappings
{
    internal class BookRatingMap : IEntityTypeConfiguration<BookRatingEntity>
    {
        public void Configure(EntityTypeBuilder<BookRatingEntity> builder)
        {
            builder.ToTable("book_rating");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("id");

            builder.Property(x => x.Rating)
                   .HasColumnName("rating");

            builder.Property(x => x.BookId)
                   .HasColumnName("book_id");

            builder.Property(x => x.UserId)
                   .HasColumnName("user_id");
        }
    }
}