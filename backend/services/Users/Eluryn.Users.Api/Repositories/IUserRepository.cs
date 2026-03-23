using Eluryn.Users.Api.Entities;

namespace Eluryn.Users.Api.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserById(Guid id, CancellationToken cancellationToken);
}