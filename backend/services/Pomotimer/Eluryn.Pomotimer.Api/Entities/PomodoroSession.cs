namespace Eluryn.Pomotimer.Api.Entities;

public class PomodoroSession
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Status { get; set; } = string.Empty;
    public string CurrentIntervalType { get; set; } = string.Empty;
    public int Phase { get; set; }

    public DateTime? StartTimeUtc { get; set; }
    public DateTime? PausedAtUtc { get; set; }
    public int AccumulatedPauseSeconds { get; set; }

    public int SnapshotFocusSeconds { get; set; }
    public int SnapshotShortBreakSeconds { get; set; }
    public int SnapshotLongBreakSeconds { get; set; }
    public int SnapshotLongBreakInterval { get; set; }

    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }

    public ICollection<FocusLogEntry> FocusLogEntries { get; set; } = new List<FocusLogEntry>();
}