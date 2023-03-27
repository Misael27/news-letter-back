namespace NewsLetter.Web.AuthConfigure
{
    public class AccessTokenDTO
    {
        public string? Token { get; set; }

        public DateTime IssuedAt { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
