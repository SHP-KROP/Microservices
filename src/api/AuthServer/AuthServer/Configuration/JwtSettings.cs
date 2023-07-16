namespace AuthServer.Configuration
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        
        public string Issuer { get; set; }

        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(7);
    }
}
