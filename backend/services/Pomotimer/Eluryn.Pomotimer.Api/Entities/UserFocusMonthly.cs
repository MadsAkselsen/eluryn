namespace Eluryn.Pomotimer.Api.Entities;

public class UserFocusMonthly
{
    public Guid UserId { get; set; }
    public DateOnly MonthStartUtc { get; set; }
    public int FocusSeconds { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}