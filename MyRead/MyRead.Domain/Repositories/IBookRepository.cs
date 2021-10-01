using MyRead.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<bool> Create(BookEntity book, CancellationToken token);
        Task Save(BookCountsEntity bookCounts, CancellationToken token);

        Task<BookEntity> ById(Guid id, CancellationToken token);
        Task<BookCountsEntity> CountsById(Guid id, CancellationToken token);
    }
}