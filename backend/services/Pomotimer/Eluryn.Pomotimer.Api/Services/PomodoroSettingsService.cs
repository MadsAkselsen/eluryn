using Eluryn.Pomotimer.Api.Dtos;
using Eluryn.Pomotimer.Api.Entities;
using Eluryn.Pomotimer.Api.Repositories;

namespace Eluryn.Pomotimer.Api.Services;

public class PomodoroSettingsService(IPomodoroSettingsRepository repository) : IPomodoroSettingsService
{
    private readonly IPomodoroSettingsRepository _repository = repository;

    public async Task<PomodoroSettingsResponseDto?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var settings = await _repository.GetByUserIdAsync(userId, cancellationToken);

        if (settings is null)
        {
            return null;
        }

        return MapToResponseDto(settings);
    }

    public async Task<PomodoroSettingsResponseDto> UpsertAsync(
        Guid userId,
        UpsertPomodoroSettingsRequestDto request,
        CancellationToken cancellationToken)
    {
        Validate(request);

        var now = DateTime.UtcNow;

        var existingSettings = await _repository.GetByUserIdAsync(userId, cancellationToken);

        if (existingSettings is null)
        {
            var newSettings = new PomodoroSettings
            {
                UserId = userId,
                FocusSeconds = request.FocusSeconds,
                ShortBreakSeconds = request.ShortBreakSeconds,
                LongBreakSeconds = request.LongBreakSeconds,
                LongBreakInterval = request.LongBreakInterval,
                CreatedAtUtc = now,
                UpdatedAtUtc = now
            };

            await _repository.AddAsync(newSettings, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return MapToResponseDto(newSettings);
        }

        existingSettings.FocusSeconds = request.FocusSeconds;
        existingSettings.ShortBreakSeconds = request.ShortBreakSeconds;
        existingSettings.LongBreakSeconds = request.LongBreakSeconds;
        existingSettings.LongBreakInterval = request.LongBreakInterval;
        existingSettings.UpdatedAtUtc = now;

        await _repository.SaveChangesAsync(cancellationToken);

        return MapToResponseDto(existingSettings);
    }

    private static void Validate(UpsertPomodoroSettingsRequestDto request)
    {
        if (request.FocusSeconds <= 0)
        {
            throw new ArgumentException("FocusSeconds must be greater than 0.");
        }

        if (request.ShortBreakSeconds <= 0)
        {
            throw new ArgumentException("ShortBreakSeconds must be greater than 0.");
        }

        if (request.LongBreakSeconds <= 0)
        {
            throw new ArgumentException("LongBreakSeconds must be greater than 0.");
        }

        if (request.LongBreakInterval <= 0)
        {
            throw new ArgumentException("LongBreakInterval must be greater than 0.");
        }
    }

    private static PomodoroSettingsResponseDto MapToResponseDto(PomodoroSettings settings)
    {
        return new PomodoroSettingsResponseDto
        {
            UserId = settings.UserId,
            FocusSeconds = settings.FocusSeconds,
            ShortBreakSeconds = settings.ShortBreakSeconds,
            LongBreakSeconds = settings.LongBreakSeconds,
            LongBreakInterval = settings.LongBreakInterval,
            CreatedAtUtc = settings.CreatedAtUtc,
            UpdatedAtUtc = settings.UpdatedAtUtc
        };
    }
}