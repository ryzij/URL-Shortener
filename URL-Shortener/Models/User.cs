namespace URL_Shortener.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
