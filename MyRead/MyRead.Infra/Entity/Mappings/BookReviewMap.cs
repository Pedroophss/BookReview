using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRead.Domain.Entities;

namespace MyRead.Infra.Entity.Mappings
{
    internal class BookReviewMap : IEntityTypeConfiguration<BookReviewEntity>
    {
        public void Configure(EntityTypeBuilder<BookReviewEntity> builder)
        {
            builder.ToTable("book_review");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("id");

            builder.Property(x => x.Reviwer)
                   .HasColumnName("review");

            builder.Property(x => x.BookId)
                   .HasColumnName("book_id");

            builder.Property(x => x.UserId)
                   .HasColumnName("user_id");
        }
    }
}