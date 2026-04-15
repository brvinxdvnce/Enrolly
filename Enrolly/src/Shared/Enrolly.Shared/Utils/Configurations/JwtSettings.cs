namespace Enrolly.Shared.Logging;

public class JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecurityKey { get; set; }
    public string SecurityAlgorithm { get; set; }
    public int ExpiresInHours { get; set; }
}