namespace Eluryn.Pomotimer.Api.Dtos;

public class PomodoroSettingsResponseDto
{
    public Guid UserId { get; set; }
    public int FocusSeconds { get; set; }
    public int ShortBreakSeconds { get; set; }
    public int LongBreakSeconds { get; set; }
    public int LongBreakInterval { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}