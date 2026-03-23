using Eluryn.Pomotimer.Api.Data;
using Eluryn.Pomotimer.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eluryn.Pomotimer.Api.Repositories;

public class PomodoroSettingsRepository : IPomodoroSettingsRepository
{
    private readonly PomotimerDbContext _dbContext;

    public PomodoroSettingsRepository(PomotimerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<PomodoroSettings?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _dbContext.PomodoroSettings
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(PomodoroSettings settings, CancellationToken cancellationToken)
    {
        await _dbContext.PomodoroSettings.AddAsync(settings, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}