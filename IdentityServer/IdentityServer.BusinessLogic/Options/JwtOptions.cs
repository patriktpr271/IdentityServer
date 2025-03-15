namespace IdentityServer.BusinessLogic.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpirationInMinutes { get; set; } = 60;
 
}