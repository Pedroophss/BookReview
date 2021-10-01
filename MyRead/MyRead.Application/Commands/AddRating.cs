using MediatR;
using MyRead.Application.Abstractions;
using MyRead.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Application.Commands
{
    public record AddRatingOutput(BookCountsEntity Counts, BookRatingEntity Rating) { }
    public record AddRatingCommand(Guid UserId, Guid BookId, byte Rating) : IRequest<AddRatingOutput>
    {
        public BookCountsEntity Counts { get; init; }
    }

    internal class AddRatingCommandHandler : IRequestHandler<AddRatingCommand, AddRatingOutput>
    {
        public AddRatingCommandHandler(IUnitOfWork uow, INotifier notifier)
        {
            Uow = uow;
            Notifier = notifier;
        }

        public IUnitOfWork Uow { get; }
        public INotifier Notifier { get; }

        public async Task<AddRatingOutput> Handle(AddRatingCommand r, CancellationToken token)
        {
            var rating = new BookRatingEntity(r.UserId, r.BookId, r.Rating);
            var validations = rating.Validate();

            if (validations.Any())
            {
                Notifier.Notify(validations);
                return null;
            }

            await Uow.Ratings.Save(rating, token);

            var counts = r.Counts ?? await Uow.Books.CountsById(r.BookId, token);
                counts.NewRating(r.Rating);

            await Uow.Books.Save(counts, token);

            return new (counts, rating);
        }
    }
}