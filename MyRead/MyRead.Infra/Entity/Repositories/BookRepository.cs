using Microsoft.EntityFrameworkCore;
using MyRead.Domain.Entities;
using MyRead.Domain.Repositories;
using MyRead.Infra.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Infra.Entity.Repositories
{
    public class BookRepository : IBookRepository, IRepository
    {
        private MyReadContext Ctx { get; }

        public BookRepository(MyReadContext ctx) =>
            Ctx = ctx;

        public async Task<BookEntity> ById(Guid id, CancellationToken token) =>
            await Ctx.Books.FirstOrDefaultAsync(f => f.Id == id, token); 

        public async Task<BookCountsEntity> CountsById(Guid id, CancellationToken token) =>
            await Ctx.BookCounts.FirstOrDefaultAsync(f => f.Id == id, token);

        public async Task<bool> Create(BookEntity book, CancellationToken token)
        {
            var hasAlready = await Ctx.Books.FirstOrDefaultAsync(f => f.Title == book.Title, token);
            if (hasAlready is not null)
                return false;

            await Ctx.Books.AddAsync(book, token);
            return true;
        }

        public async Task Save(BookCountsEntity bookCounts, CancellationToken token) =>
            await Ctx.BookCounts.AddAsync(bookCounts, token);
    }
}