namespace URL_Shortener.Models
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortCode { get; set; } = string.Empty;
        public int TotalClickCount { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
        public DateTime? ExpirationDateTime { get; set; } = null;
        public int ClickLimit { get; set; } = 0;
    }
}
