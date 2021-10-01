using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRead.Domain.Entities;

namespace MyRead.Infra.Entity.Mappings
{
    internal class BookMap : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable("book");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("id");

            builder.Property(x => x.Language)
                   .HasColumnName("language_code");

            builder.Property(x => x.Pages)
                   .HasColumnName("num_pages");

            builder.Property(x => x.Title)
                   .HasColumnName("title");

            builder.Property(x => x.Title)
                   .HasColumnName("title");

            builder.Property(x => x.Authors)
                   .HasColumnName("authors");
        }
    }
}