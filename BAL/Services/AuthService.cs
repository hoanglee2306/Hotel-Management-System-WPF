using BAL.Interfaces;
using DAL;
using DAL.Models;
using DAL.Repository;

namespace BAL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ValidateLogin(string email, string password)
        {
            var user = _userRepository.Authenticate(email, password);
            return user != null;
        }

        public bool IsAdmin(string email)
        {
            var user = _userRepository.GetByEmail(email);
            return user != null && user.CustomerType == (int)CustomerType.Admin;
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }
    }
}