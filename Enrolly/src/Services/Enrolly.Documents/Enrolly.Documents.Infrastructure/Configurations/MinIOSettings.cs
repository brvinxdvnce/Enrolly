namespace Enrolly.Documents.Infrastructure.Configurations;

public class MinIOSettings
{
    public string Address  { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public ICollection<string> Buckets { get; set; }
    public string Region { get; set; }
    public bool WithSSL { get; set; }
}