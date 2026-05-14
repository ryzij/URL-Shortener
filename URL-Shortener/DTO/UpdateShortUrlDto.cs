using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace URL_Shortener.DTO
{
    public class UpdateShortUrlDto
    {
        public string? OriginalUrl { get; set; }

        public DateTime? ExpirationDateTime { get; set; } = null;

        public int? ClickLimit { get; set; } = null;

        [DefaultValue(false)]
        public bool ResetExpirationDateTime { get; set; } = false;
    }
}
