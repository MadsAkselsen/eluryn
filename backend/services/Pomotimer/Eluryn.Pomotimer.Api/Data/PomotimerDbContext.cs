using Eluryn.Pomotimer.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eluryn.Pomotimer.Api.Data;

public class PomotimerDbContext(DbContextOptions<PomotimerDbContext> options) : DbContext(options)
{
    public DbSet<PomodoroSettings> PomodoroSettings => Set<PomodoroSettings>();
    public DbSet<UserPreferences> UserPreferences => Set<UserPreferences>();
    public DbSet<PomodoroSession> PomodoroSessions => Set<PomodoroSession>();
    public DbSet<FocusLogEntry> FocusLogEntries => Set<FocusLogEntry>();
    public DbSet<UserFocusDaily> UserFocusDaily => Set<UserFocusDaily>();
    public DbSet<UserFocusWeekly> UserFocusWeekly => Set<UserFocusWeekly>();
    public DbSet<UserFocusMonthly> UserFocusMonthly => Set<UserFocusMonthly>();
    public DbSet<UserFocusAllTime> UserFocusAllTime => Set<UserFocusAllTime>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurePomodoroSettings(modelBuilder);
        ConfigureUserPreferences(modelBuilder);
        ConfigurePomodoroSessions(modelBuilder);
        ConfigureFocusLogEntries(modelBuilder);
        ConfigureUserFocusDaily(modelBuilder);
        ConfigureUserFocusWeekly(modelBuilder);
        ConfigureUserFocusMonthly(modelBuilder);
        ConfigureUserFocusAllTime(modelBuilder);
    }

    private static void ConfigurePomodoroSettings(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PomodoroSettings>(entity =>
        {
            entity.ToTable("pomodoro_settings", "pomotimer");

            entity.HasKey(x => x.UserId);

            entity.Property(x => x.UserId).HasColumnName("user_id");
            entity.Property(x => x.FocusSeconds).HasColumnName("focus_seconds");
            entity.Property(x => x.ShortBreakSeconds).HasColumnName("short_break_seconds");
            entity.Property(x => x.LongBreakSeconds).HasColumnName("long_break_seconds");
            entity.Property(x => x.LongBreakInterval).HasColumnName("long_break_interval");
            entity.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");
            entity.Property(x => x.UpdatedAtUtc).HasColumnName("updated_at_utc");
        });
    }

    private static void ConfigureUserPreferences(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPreferences>(entity =>
        {
            entity.ToTable("user_preferences", "pomotimer");

            entity.HasKey(x => x.UserId);

            entity.Property(x => x.UserId).HasColumnName("user_id");
            entity.Property(x => x.Theme).HasColumnName("theme");
            entity.Property(x => x.SoundEnabled).HasColumnName("sound_enabled");
            entity.Property(x => x.AutoStartNextInterval).HasColumnName("auto_start_next_interval");
            entity.Property(x => x.Locale).HasColumnName("locale");
            entity.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");
            entity.Property(x => x.UpdatedAtUtc).HasColumnName("updated_at_utc");
        });
    }

    private static void ConfigurePomodoroSessions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PomodoroSession>(entity =>
        {
            entity.ToTable("pomodoro_sessions", "pomotimer");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id).HasColumnName("id");
            entity.Property(x => x.UserId).HasColumnName("user_id");

            entity.Property(x => x.Status).HasColumnName("status");
            entity.Property(x => x.CurrentIntervalType).HasColumnName("current_interval_type");
            entity.Property(x => x.Phase).HasColumnName("phase");

            entity.Property(x => x.StartTimeUtc).HasColumnName("start_time_utc");
            entity.Property(x => x.PausedAtUtc).HasColumnName("paused_at_utc");
            entity.Property(x => x.AccumulatedPauseSeconds).HasColumnName("accumulated_pause_seconds");

            entity.Property(x => x.SnapshotFocusSeconds).HasColumnName("snapshot_focus_seconds");
            entity.Property(x => x.SnapshotShortBreakSeconds).HasColumnName("snapshot_short_break_seconds");
            entity.Property(x => x.SnapshotLongBreakSeconds).HasColumnName("snapshot_long_break_seconds");
            entity.Property(x => x.SnapshotLongBreakInterval).HasColumnName("snapshot_long_break_interval");

            entity.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");
            entity.Property(x => x.UpdatedAtUtc).HasColumnName("updated_at_utc");
        });
    }

    private static void ConfigureFocusLogEntries(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FocusLogEntry>(entity =>
        {
            entity.ToTable("focus_log_entries", "pomotimer");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id).HasColumnName("id");
            entity.Property(x => x.UserId).HasColumnName("user_id");
            entity.Property(x => x.SessionId).HasColumnName("session_id");

            entity.Property(x => x.IntervalType).HasColumnName("interval_type");
            entity.Property(x => x.StartedAtUtc).HasColumnName("started_at_utc");
            entity.Property(x => x.EndedAtUtc).HasColumnName("ended_at_utc");
            entity.Property(x => x.DurationSeconds).HasColumnName("duration_seconds");
            entity.Property(x => x.ActiveSeconds).HasColumnName("active_seconds");
            entity.Property(x => x.PauseSeconds).HasColumnName("pause_seconds");
            entity.Property(x => x.EndedEarly).HasColumnName("ended_early");
            entity.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");

            entity.HasOne(x => x.Session)
                .WithMany(x => x.FocusLogEntries)
                .HasForeignKey(x => x.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureUserFocusDaily(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFocusDaily>(entity =>
        {
            entity.ToTable("user_focus_daily", "pomotimer");

            entity.HasKey(x => new { x.UserId, x.DateUtc });

            entity.Property(x => x.UserId).HasColumnName("user_id");
            entity.Property(x => x.DateUtc).HasColumnName("date_utc");
            entity.Property(x => x.FocusSeconds).HasColumnName("focus_seconds");
            entity.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");
            entity.Property(x => x.UpdatedAtUtc).HasColumnName("updated_at_utc");
        });
    }

    private static void ConfigureUserFocusWeekly(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFocusWeekly>(entity =>
        {
            entity.ToTable("user_focus_weekly", "pomotimer");

            entity.HasKey(x => new { x.UserId, x.WeekStartUtc });

            entity.Property(x => x.UserId).HasColumnName("user_id");
            entity.Property(x => x.WeekStartUtc).HasColumnName("week_start_utc");
            entity.Property(x => x.FocusSeconds).HasColumnName("focus_seconds");
            entity.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");
            entity.Property(x => x.UpdatedAtUtc).HasColumnName("updated_at_utc");
        });
    }

    private static void ConfigureUserFocusMonthly(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFocusMonthly>(entity =>
        {
            entity.ToTable("user_focus_monthly", "pomotimer");

            entity.HasKey(x => new { x.UserId, x.MonthStartUtc });

            entity.Property(x => x.UserId).HasColumnName("user_id");
            entity.Property(x => x.MonthStartUtc).HasColumnName("month_start_utc");
            entity.Property(x => x.FocusSeconds).HasColumnName("focus_seconds");
            entity.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");
            entity.Property(x => x.UpdatedAtUtc).HasColumnName("updated_at_utc");
        });
    }

    private static void ConfigureUserFocusAllTime(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFocusAllTime>(entity =>
        {
            entity.ToTable("user_focus_all_time", "pomotimer");

            entity.HasKey(x => x.UserId);

            entity.Property(x => x.UserId).HasColumnName("user_id");
            entity.Property(x => x.FocusSeconds).HasColumnName("focus_seconds");
            entity.Property(x => x.CreatedAtUtc).HasColumnName("created_at_utc");
            entity.Property(x => x.UpdatedAtUtc).HasColumnName("updated_at_utc");
        });
    }
}