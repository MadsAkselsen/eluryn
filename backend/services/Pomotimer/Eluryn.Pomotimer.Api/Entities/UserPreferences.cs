namespace Eluryn.Pomotimer.Api.Entities;

public class UserPreferences
{
    public Guid UserId { get; set; }
    public string Theme { get; set; } = string.Empty;
    public bool SoundEnabled { get; set; }
    public bool AutoStartNextInterval { get; set; }
    public string Locale { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}