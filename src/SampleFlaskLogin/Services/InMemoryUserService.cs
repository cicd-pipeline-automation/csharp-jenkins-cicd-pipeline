using Microsoft.Extensions.Configuration;

namespace SampleFlaskLogin.Services
{
    /// <summary>
    /// Simple in-memory user validation service.
    /// Loads demo credentials from configuration:
    ///   Authentication:DemoUser:Username
    ///   Authentication:DemoUser:Password
    /// </summary>
    public class InMemoryUserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public InMemoryUserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Validates username and password against configured demo credentials.
        /// </summary>
        public bool ValidateUser(string username, string password)
        {
            var demoUser = _configuration.GetSection("Authentication:DemoUser");

            var expectedUser = demoUser["Username"];
            var expectedPass = demoUser["Password"];

            return username == expectedUser && password == expectedPass;
        }
    }
}
