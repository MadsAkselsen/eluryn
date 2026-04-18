namespace Eluryn.Users.Api.Dtos;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}