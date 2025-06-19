using System.IO;
using Microsoft.Extensions.Configuration;

namespace FUMiniHotelSystem.Utils
{
    public static class AppConfig
    {
        private static IConfigurationRoot _configuration;

        static AppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static string GetAdminEmail() => _configuration["DefaultAdmin:Email"];

        public static string GetAdminPassword() => _configuration["DefaultAdmin:Password"];
    }
}