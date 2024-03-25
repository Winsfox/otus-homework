using System.Threading;
using System.Threading.Tasks;

namespace Otus.Highload.Homework.Users;

public interface IUserRepository
{
    Task<long> AddAsync(User user, CancellationToken cancellationToken);

    Task<User?> GetAsync(long id, CancellationToken cancellationToken);
}
