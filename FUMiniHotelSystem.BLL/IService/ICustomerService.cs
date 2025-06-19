using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.BLL.IService
{
    public interface ICustomerService
    {
        List<Customer> GetAll();

        Customer? GetById(int id);

        Customer? GetByEmail(string email);

        void Add(Customer customer);

        void Update(Customer customer);

        void Delete(int id);
    }
}