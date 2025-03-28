using DAL.Models;

namespace BAL.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        bool DeleteCustomer(int id);
        IEnumerable<Customer> SearchCustomers(string searchTerm);
        
        // For customer to manage their own profile
        void UpdateCustomerProfile(Customer customer);
    }
}