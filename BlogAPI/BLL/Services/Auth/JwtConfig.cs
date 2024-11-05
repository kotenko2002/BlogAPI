namespace BlogAPI.BLL.Services.Auth
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int TokenValidityInDays { get; set; }
    }
}
