using MediatR;
using MyRead.Application.Abstractions;
using MyRead.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Application.Commands
{
    public record AddUserCommand(string Name, uint Age, bool IsProfessional)
        : IRequest<UserEntity> { }

    internal class AddUserHandler : IRequestHandler<AddUserCommand, UserEntity>
    {
        public AddUserHandler(IUnitOfWork uow, INotifier notifier)
        {
            Uow = uow;
            Notifier = notifier;
        }

        public IUnitOfWork Uow { get; }
        public INotifier Notifier { get; }

        public async Task<UserEntity> Handle(AddUserCommand r, CancellationToken token)
        {
            var user = new UserEntity(r.Name, r.Age, r.IsProfessional);
            var validations = user.Validate();

            if (validations.Any())
            {
                Notifier.Notify(validations);
                return null;
            }

            await Uow.Users.Save(user, token);
            return user;
        }
    }
}