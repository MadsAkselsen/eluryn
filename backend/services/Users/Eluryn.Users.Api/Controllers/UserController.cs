using Eluryn.Users.Api.Dtos;
using Eluryn.Users.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eluryn.Users.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UsersController(IUserService service, ILogger<UsersController> logger) : ControllerBase
{
    private readonly IUserService _service = service;
    private readonly ILogger<UsersController> _logger = logger;

    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDto>> GetByUserId(
        Guid userId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching user {UserId}", userId);

        var result = await _service.GetByUserIdAsync(userId, cancellationToken);

        if (result is null)
        {
            _logger.LogWarning("User {UserId} not found", userId);
            return NotFound();
        }

        return Ok(result);
    }
}