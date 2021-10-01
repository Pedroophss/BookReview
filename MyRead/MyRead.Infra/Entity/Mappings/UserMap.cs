using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRead.Domain.Entities;

namespace MyRead.Infra.Entity.Mappings
{
    internal class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("user");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("id");

            builder.Property(x => x.IsProfessional)
                   .HasColumnName("is_professional")
                   .HasConversion(entity_db => entity_db ? 1 : 0, db_entity => db_entity == 1);

            builder.Property(x => x.Age)
                   .HasColumnName("age");

            builder.Property(x => x.Name)
                   .HasColumnName("name")
                   .HasMaxLength(100);
        }
    }
}