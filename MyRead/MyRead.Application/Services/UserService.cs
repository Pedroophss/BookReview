using MediatR;
using MyRead.Application.Abstractions;
using MyRead.Application.Notifications;
using MyRead.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Application.Services
{
    public interface IUserService : IService
    {
        Task<bool> NewUser(UserEntity user, CancellationToken token);
    }

    internal class UserService : IUserService
    {
        public UserService(IMediator mediator, IUnitOfWork uow, INotifier notifier)
        {
            Uow = uow;
            Notifier = notifier;
            Mediator = mediator;
        }

        public IUnitOfWork Uow { get; }
        public INotifier Notifier { get; }
        public IMediator Mediator { get; }

        public async Task<bool> NewUser(UserEntity user, CancellationToken token)
        {
            Notifier.Notify(user.Validate());
            if (Notifier.HasError)
                return false;

            await Uow.Users.Save(user, token);

            user.AfterCommited += async args =>
                await Mediator.Publish(new SendWelcomeMessage(args.Entity as UserEntity));

            return true;
        }
    }
}