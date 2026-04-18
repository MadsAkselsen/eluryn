using Eluryn.Users.Api.Dtos;
using Eluryn.Users.Api.Entities;
using Eluryn.Users.Api.Repositories;

namespace Eluryn.Users.Api.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _repository = userRepository;

    public async Task<UserResponseDto?> GetByUserIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserById(id, cancellationToken);

        if (user is null)
        {
            return null;
        }

        return MapToResponseDto(user);
    }

    private static UserResponseDto MapToResponseDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            CreatedAtUtc = user.CreatedAtUtc,
            UpdatedAtUtc = user.UpdatedAtUtc,
        };
    }
}