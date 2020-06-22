using Microsoft.Extensions.Configuration;

namespace TEKApp.Services
{
    public class AppSettingService : IAppSettingService
    {
        private IConfiguration _configuration;

        public AppSettingService()
        {
            _configuration = CreateContext();
        }    
        public int MaxOrder => int.Parse(_configuration.GetSection("MaxOrder").GetSection("Default").Value);

        private IConfigurationRoot CreateContext()
        {
            return new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", true, true)
          .Build();
        }

    }
}
