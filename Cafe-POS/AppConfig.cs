public class AppConfig
{
    private readonly IConfiguration _configuration;

    public AppConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string ConnectionString => _configuration.GetConnectionString("CafeDb");
}