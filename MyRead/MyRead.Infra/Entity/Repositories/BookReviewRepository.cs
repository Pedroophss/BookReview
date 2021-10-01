using Microsoft.EntityFrameworkCore;
using MyRead.Domain.Entities;
using MyRead.Domain.Repositories;
using MyRead.Infra.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRead.Infra.Entity.Repositories
{
    internal class BookReviewRepository : IBookReviewRepository, IRepository
    {
        private MyReadContext Ctx { get; }

        public BookReviewRepository(MyReadContext ctx) =>
            Ctx = ctx;

        public async Task<List<BookReviewEntity>> ByBook(Guid bookId) =>
            await Ctx.Reviwers.Where(w => w.BookId == bookId).ToListAsync();

        public async Task<List<BookReviewEntity>> ByUser(Guid userId) =>
            await Ctx.Reviwers.Where(w => w.UserId == userId).ToListAsync();

        public async Task<BookReviewEntity> ByUserAndBook(Guid userId, Guid bookId) =>
            await Ctx.Reviwers.FirstOrDefaultAsync(w => w.BookId == bookId && w.UserId == userId);

        public async Task Save(BookReviewEntity review) =>
            await Ctx.Reviwers.AddAsync(review);
    }
}