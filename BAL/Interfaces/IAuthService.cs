using DAL.Models;

namespace BAL.Interfaces
{
    public interface IAuthService
    {
        bool ValidateLogin(string email, string password);
        bool IsAdmin(string email);
        Customer GetCustomerByEmail(string email);
    }
}