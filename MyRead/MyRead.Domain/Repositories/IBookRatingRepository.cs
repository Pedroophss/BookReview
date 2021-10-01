using MyRead.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Domain.Repositories
{
    public interface IBookRatingRepository
    {
        Task Save(BookRatingEntity review, CancellationToken token);

        Task<BookRatingEntity> ByUserAndBook(Guid userId, Guid bookId, CancellationToken token);

        Task<List<BookRatingEntity>> ByUser(Guid userId, CancellationToken token);

        Task<List<BookRatingEntity>> ByBook(Guid bookId, CancellationToken token);
    }
}