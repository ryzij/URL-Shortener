using System.ComponentModel.DataAnnotations;

namespace URL_Shortener.DTO
{
    public class CreateShortUrlDto
    {
        [Required]
        [Url]
        public string OriginalUrl { get; set; } = string.Empty;

        public DateTime? ExpirationDateTime { get; set; } = null;

        public int ClickLimit { get; set; } = 0;
    }
}
