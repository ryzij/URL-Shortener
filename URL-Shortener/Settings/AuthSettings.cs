namespace URL_Shortener.Settings
{
    public class AuthSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public TimeSpan Expires { get; set; }
    }
}
