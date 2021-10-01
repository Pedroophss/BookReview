using MediatR;
using MyRead.Application.Abstractions;
using MyRead.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Application.Commands.AddBook
{
    public record AddBookCommand(string Title, string Authors, string Language, int Pages)
        : IRequest<BookEntity> { }

    internal class AddBookCommandHandler : IRequestHandler<AddBookCommand, BookEntity>
    {
        public AddBookCommandHandler(IUnitOfWork uow, INotifier notifier)
        {
            Uow = uow;
            Notifier = notifier;
        }

        public IUnitOfWork Uow { get; }
        public INotifier Notifier { get; }

        public async Task<BookEntity> Handle(AddBookCommand r, CancellationToken cancellationToken)
        {
            var book = new BookEntity(r.Title, r.Authors, r.Language, r.Pages);
            var validations = book.Validate();

            if (validations.Any())
            {
                Notifier.Notify(validations);
                return null;
            }

            await Uow.Books.Create(book, cancellationToken);
            return book;
        }
    }
}