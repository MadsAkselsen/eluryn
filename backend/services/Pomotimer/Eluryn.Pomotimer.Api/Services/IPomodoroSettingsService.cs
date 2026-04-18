using Eluryn.Pomotimer.Api.Dtos;

namespace Eluryn.Pomotimer.Api.Services;

public interface IPomodoroSettingsService
{
    Task<PomodoroSettingsResponseDto?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<PomodoroSettingsResponseDto> UpsertAsync(
        Guid userId,
        UpsertPomodoroSettingsRequestDto request,
        CancellationToken cancellationToken);
}