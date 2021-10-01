using MyRead.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyRead.Domain.Repositories
{
    public interface IUserRepository
    {
        Task Save(UserEntity user, CancellationToken token);
    }
}