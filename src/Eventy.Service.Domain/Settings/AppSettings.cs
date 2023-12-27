namespace Eventy.Service.Domain.Settings
{
    public class AppSettings
    {
        public string PostgreSQLConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ApplicationName { get; set; }
        public string JwtSecretKey { get; set; }
    }
}
