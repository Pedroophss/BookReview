using Microsoft.EntityFrameworkCore;
using MyRead.Domain.Entities;
using MyRead.Domain.Repositories;
using MyRead.Infra.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Infra.Entity.Repositories
{
    internal class BookRatingRepository : IBookRatingRepository, IRepository
    {
        public BookRatingRepository(MyReadContext ctx) =>
            Ctx = ctx;

        public MyReadContext Ctx { get; }

        public async Task<List<BookRatingEntity>> ByBook(Guid bookId, CancellationToken token) =>
            await Ctx.Ratings.Where(w => w.BookId == bookId).ToListAsync(token);

        public async Task<List<BookRatingEntity>> ByUser(Guid userId, CancellationToken token) =>
            await Ctx.Ratings.Where(w => w.UserId == userId).ToListAsync(token);

        public async Task<BookRatingEntity> ByUserAndBook(Guid userId, Guid bookId, CancellationToken token) =>
            await Ctx.Ratings.Where(w => w.UserId == userId && w.BookId == bookId).FirstOrDefaultAsync(token);

        public async Task Save(BookRatingEntity review, CancellationToken token) =>
            await Ctx.Ratings.AddAsync(review, token);
    }
}