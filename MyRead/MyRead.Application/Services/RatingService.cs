using Microsoft.Extensions.Logging;
using MyRead.Application.Abstractions;
using MyRead.Domain.Entities;
using MyRead.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Application.Services
{
    public interface IRatingService : IService
    {
        Task<bool> SaveRating(BookRatingEntity rating, CancellationToken token);
    }

    internal class RatingService : IRatingService
    {
        public RatingService(IUnitOfWork uow, INotifier notifier)
        {
            Uow = uow;
            Notifier = notifier;
        }

        private IUnitOfWork Uow { get; }
        private INotifier Notifier { get; }

        public async Task<bool> SaveRating(BookRatingEntity rating, CancellationToken token)
        {
            Notifier.Notify(rating.Validate());
            if (Notifier.HasError)
                return false;

            var counts = await Uow.Books.CountsById(rating.BookId, token);
            if (counts is null)
            {
                Notifier.Notify(new ValidationItem(nameof(RatingService), $"The book {rating.BookId} does not exists", LogLevel.Information));
                return false;
            }

            await Uow.Ratings.Save(rating, token);

            counts.NewRating(rating.Rating);
            await Uow.Books.Save(counts, token);

            return true;
        }
    }
}