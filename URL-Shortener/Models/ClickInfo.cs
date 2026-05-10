namespace URL_Shortener.Models
{
    public class ClickInfo
    {
        public int Id { get; set; }
        public int ShortUrlId { get; set; }
        public DateTime ClickDateTime { get; set; } = DateTime.UtcNow;
    }
}
