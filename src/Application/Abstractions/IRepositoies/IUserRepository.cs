using Cinema.Domain.Entities;

namespace Cinema.Application.Abstractions.IRepositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByNameAsync(string name, CancellationToken token = default);

    Task<User?> GetByIdWithNavigationPropertyAsync(Guid id, CancellationToken tokne = default);
}
