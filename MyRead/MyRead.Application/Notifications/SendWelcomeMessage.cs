using MediatR;
using MyRead.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Application.Notifications
{
    public record SendWelcomeMessage(UserEntity User)
        : INotification { }

    internal class SendWelcomeMessageHandler : INotificationHandler<SendWelcomeMessage>
    {
        public Task Handle(SendWelcomeMessage notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
