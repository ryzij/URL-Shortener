using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("/go")]
    public class RedirectController(AppDbContext db) : ControllerBase
    {
        private readonly AppDbContext _db = db;

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToOriginalAsync(string shortCode)
        {
            var shortUrl = await _db.ShortUrls.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
            if (shortUrl == null)
                return NotFound("Short URL not found");

            if (shortUrl.ExpirationDateTime.HasValue && shortUrl.ExpirationDateTime.Value < DateTime.UtcNow ||
                shortUrl.ClickLimit > 0 && shortUrl.TotalClickCount >= shortUrl.ClickLimit)
                return BadRequest("Short URL has expired");

            var clickInfo = new ClickInfo
            {
                ShortUrlId = shortUrl.Id,
                ClickDateTime = DateTime.UtcNow
            };
            shortUrl.TotalClickCount++;
            _db.ClickInfos.Add(clickInfo);
            await _db.SaveChangesAsync();

            return Redirect(shortUrl.OriginalUrl);
        }
    }
}
