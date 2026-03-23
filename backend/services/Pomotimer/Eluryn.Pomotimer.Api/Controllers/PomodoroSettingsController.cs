namespace Eluryn.Pomotimer.Api.Controllers;

[ApiController]
[Route("api/pomodoro-settings")]
public class PomodoroSettingsController : ControllerBase
{
    private readonly IPomodoroSettingsService _service;

    public PomodoroSettingsController(IPomodoroSettingsService service)
    {
        _service = service;
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(PomodoroSettingsResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PomodoroSettingsResponseDto>> GetByUserId(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetByUserIdAsync(userId, cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut("{userId:guid}")]
    [ProducesResponseType(typeof(PomodoroSettingsResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PomodoroSettingsResponseDto>> Upsert(
        Guid userId,
        [FromBody] UpsertPomodoroSettingsRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.UpsertAsync(userId, request, cancellationToken);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}