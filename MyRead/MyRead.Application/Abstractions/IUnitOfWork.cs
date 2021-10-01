using MyRead.Domain.Entities;
using MyRead.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Application.Abstractions
{
    // Aplicação do UOW pattern
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }
        public IBookRepository Books { get; }
        public IBookRatingRepository Ratings { get; }
        public IBookReviewRepository Reviwers { get; }

        public Task Commit(CancellationToken token);
        public Task Commit<TEntity>() where TEntity : Entity;

        public Task Rollback();
        public Task Rollback<TEntity>() where TEntity : Entity;
    }
}