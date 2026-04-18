namespace Eluryn.Pomotimer.Api.Entities;

public class FocusLogEntry
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }

    public string IntervalType { get; set; } = string.Empty;
    public DateTime StartedAtUtc { get; set; }
    public DateTime? EndedAtUtc { get; set; }
    public int DurationSeconds { get; set; }
    public int ActiveSeconds { get; set; }
    public int PauseSeconds { get; set; }
    public bool EndedEarly { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public PomodoroSession Session { get; set; } = null!;
}