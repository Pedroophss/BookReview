using MyRead.Domain.Entities;
using MyRead.Domain.Repositories;
using MyRead.Infra.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Infra.Entity.Repositories
{
    public class UserRepository : IUserRepository, IRepository
    {
        private MyReadContext Ctx { get; }

        public UserRepository(MyReadContext ctx) =>
            Ctx = ctx;

        public async Task Save(UserEntity user, CancellationToken token) =>
            await Ctx.Users.AddAsync(user, token);
    }
}