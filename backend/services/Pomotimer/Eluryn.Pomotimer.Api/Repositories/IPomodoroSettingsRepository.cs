using Eluryn.Pomotimer.Api.Entities;

namespace Eluryn.Pomotimer.Api.Repositories;

public interface IPomodoroSettingsRepository
{
    Task<PomodoroSettings?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task AddAsync(PomodoroSettings settings, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}