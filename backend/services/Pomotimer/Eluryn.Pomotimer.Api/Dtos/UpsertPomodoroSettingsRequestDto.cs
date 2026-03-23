namespace Eluryn.Pomotimer.Api.Dtos;

public class UpsertPomodoroSettingsRequestDto
{
    public int FocusSeconds { get; set; }
    public int ShortBreakSeconds { get; set; }
    public int LongBreakSeconds { get; set; }
    public int LongBreakInterval { get; set; }
}