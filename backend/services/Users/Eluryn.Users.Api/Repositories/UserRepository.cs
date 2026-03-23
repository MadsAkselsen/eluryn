using Eluryn.Users.Api.Data;
using Eluryn.Users.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eluryn.Users.Api.Repositories;

public class UserRepository(UsersDbContext dbContext) : IUserRepository
{
    private readonly UsersDbContext _dbContext = dbContext;

    public Task<User?> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}