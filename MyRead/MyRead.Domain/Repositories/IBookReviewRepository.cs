using MyRead.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRead.Domain.Repositories
{
    public interface IBookReviewRepository
    {
        Task Save(BookReviewEntity review);

        Task<BookReviewEntity> ByUserAndBook(Guid userId, Guid bookId);

        Task<List<BookReviewEntity>> ByUser(Guid userId);

        Task<List<BookReviewEntity>> ByBook(Guid bookId);
    }
}