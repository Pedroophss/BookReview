using MediatR;
using MyRead.Application.Abstractions;
using MyRead.Application.Services;
using MyRead.Domain.Repositories;
using NSubstitute;

namespace MyRead.UnitTests.Cases.Application.v2.Services
{
    public class UserSvcMockBuilder
    {
        public UserSvcMockBuilder()
        {
            Uow = Substitute.For<IUnitOfWork>();
            Mediator = Substitute.For<IMediator>();
            Notifier = Substitute.For<INotifier>();

            Service = new UserService(Mediator, Uow, Notifier);
        }

        public IUnitOfWork Uow { get; }
        public IMediator Mediator { get; }
        public INotifier Notifier { get; }
        public IUserService Service { get; }
        public IUserRepository UserRepo { get; set; }

        public UserSvcMockBuilder SetNotifierHasErros(bool value)
        {
            Notifier.HasError.Returns(value);
            return this;
        }

        public UserSvcMockBuilder SetUowUserRepository()
        {
            UserRepo = Substitute.For<IUserRepository>();
            Uow.Users.Returns(UserRepo);

            return this;
        }
    }
}