using MyRead.Application.Abstractions;
using MyRead.Domain.Repositories;
using MyRead.Infra.Entity.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Infra.Entity
{
    internal class UnitOfWork : IUnitOfWork
    {
        private Lazy<IUserRepository> UserLazy { get; }
        public IUserRepository Users => UserLazy.Value;

        private Lazy<IBookRepository> BookLazy { get; }
        public IBookRepository Books => BookLazy.Value;

        private Lazy<IBookRatingRepository> BookRatingLazy { get; }
        public IBookRatingRepository Ratings => BookRatingLazy.Value;

        private Lazy<IBookReviewRepository> BookReviewLazy { get; }
        public IBookReviewRepository Reviwers => BookReviewLazy.Value;

        public UnitOfWork(MyReadContext ctx)
        {
            Ctx = ctx;
            UserLazy = new(() => new UserRepository(ctx));
            BookLazy = new(() => new BookRepository(ctx));
            BookRatingLazy = new(() => new BookRatingRepository(ctx));
            BookReviewLazy = new(() => new BookReviewRepository(ctx));
        }

        MyReadContext Ctx { get; }

        public async Task Commit(CancellationToken token) =>
            await Ctx.SaveChangesAsync(token);

        public Task Commit<TEntity>() where TEntity : Domain.Entities.Entity
        {
            throw new NotImplementedException();
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }

        public Task Rollback<TEntity>() where TEntity : Domain.Entities.Entity
        {
            throw new NotImplementedException();
        }
    }
}