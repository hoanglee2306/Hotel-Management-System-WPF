using BAL.Interfaces;
using DAL;
using DAL.Models;
using DAL.Repository;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace BAL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
            
            // Load configuration from appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            _configuration = builder.Build();
        }

        public bool ValidateLogin(string email, string password)
        {
            // Check if login matches the default admin account using LINQ
            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];
            
            var isDefaultAdmin = new[] { new { Email = adminEmail, Password = adminPassword } }
                .Where(a => a.Email == email && a.Password == password)
                .Any();
                
            if (isDefaultAdmin)
            {
                return true;
            }

            // Otherwise check database
            var user = _userRepository.Authenticate(email, password);
            return user != null;
        }

        public bool IsAdmin(string email)
        {
            // Check if it's the default admin email
            var adminEmail = _configuration["AdminAccount:Email"];
            if (email == adminEmail)
            {
                return true;
    }
            
            // Otherwise check database
            var user = _userRepository.GetByEmail(email);
            return user != null && user.CustomerType == (int)CustomerType.Admin;
}

        public Customer GetCustomerByEmail(string email)
        {
            // Check if it's the default admin email
            var adminEmail = _configuration["AdminAccount:Email"];
            
            // Use LINQ to create a default admin Customer object if email matches
            var defaultAdmin = new[] { adminEmail }
                .Where(e => e == email)
                .Select(_ => new Customer
                {
                    EmailAddress = adminEmail,
                    CustomerType = (int)CustomerType.Admin,
                    CustomerFullName = "Default Administrator",
                    CustomerId = 0, // Use a default ID
                    Telephone = "Admin Phone",
                })
                .FirstOrDefault();
                
            if (defaultAdmin != null)
            {
                return defaultAdmin;
    }
            
            // Otherwise get from database
            return _userRepository.GetByEmail(email);
}
    }
}
