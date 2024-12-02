using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.Services;

[ApiController]
[Route("api/[controller]")]
public class UrlShortenerController : ControllerBase
{
    private readonly URLShortenerService _urlShortenerService;

    public UrlShortenerController(URLShortenerService urlShortenerService)
    {
        _urlShortenerService = urlShortenerService;
    }

    [HttpPost("shorten")]
    public async Task<IActionResult> ShortenUrl([FromBody] string longUrl)
    {
        if (string.IsNullOrEmpty(longUrl))
            return BadRequest("Long URL cannot be empty.");

        var shortUrl = await _urlShortenerService.GetOrCreateShortUrlAsync(longUrl);
        return Ok(new { ShortUrl = $"{Request.Scheme}://{Request.Host}/expand/{shortUrl}" });
    }

    [HttpGet("expand/{shortUrl}")]
    public async Task<IActionResult> ExpandUrl(string shortUrl)
    {
        if (string.IsNullOrEmpty(shortUrl))
            return BadRequest("Short URL cannot be empty.");

        var longUrl = await _urlShortenerService.GetLongUrlAsync(shortUrl);
        if (longUrl == null)
            return NotFound("Short URL not found.");

        return Redirect(longUrl);
    }
}
