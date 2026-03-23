namespace Eluryn.Pomotimer.Api.Entities;

public class UserFocusWeekly
{
    public Guid UserId { get; set; }
    public DateOnly WeekStartUtc { get; set; }
    public int FocusSeconds { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}