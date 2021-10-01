using Microsoft.EntityFrameworkCore;
using MyRead.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Infra.Entity
{
    public class MyReadContext : DbContext
    {
        public MyReadContext(DbContextOptions options)
            : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<BookRatingEntity> Ratings { get; set; }
        public DbSet<BookReviewEntity> Reviwers { get; set; }
        public DbSet<BookCountsEntity> BookCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().ToList();
            var output = await base.SaveChangesAsync(cancellationToken);

            foreach (var entity in entries)
                if (entity.Entity is Domain.Entities.Entity item)
                    await item.TriggerAfterCommited(new Domain.Delegates.PersistedEventArgs(item));

            return output;
        }
    }
}