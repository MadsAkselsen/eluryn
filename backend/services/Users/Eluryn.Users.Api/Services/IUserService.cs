using Eluryn.Users.Api.Dtos;

namespace Eluryn.Users.Api.Services;

public interface IUserService
{
    Task<UserResponseDto?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken);
}