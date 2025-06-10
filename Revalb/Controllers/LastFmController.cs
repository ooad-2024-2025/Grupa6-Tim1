using Microsoft.AspNetCore.Mvc;
using Revalb.Services;

namespace Revalb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LastFmController : ControllerBase
{
    private readonly LastFmService _lastFmService;

    public LastFmController(LastFmService lastFmService)
    {
        _lastFmService = lastFmService;
    }

    [HttpGet("tracks")]
    public async Task<IActionResult> GetTopTracks()
    {
        var result = await _lastFmService.GetTopTracksAsync();
        return Ok(result);

    }

    [HttpGet("artists")]
    public async Task<IActionResult> GetTopArtists()
    {
        var result = await _lastFmService.GetTopArtistsAsync();
        return Ok(result);

    }
}
