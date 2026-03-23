namespace Eluryn.Pomotimer.Api.Entities;

public class UserFocusDaily
{
    public Guid UserId { get; set; }
    public DateOnly DateUtc { get; set; }
    public int FocusSeconds { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}