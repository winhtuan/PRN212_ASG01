using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.DAL.IRepository
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();

        Customer? GetById(int id);

        Customer? GetByEmail(string email);

        void Add(Customer customer);

        void Update(Customer customer);

        void Delete(int id);
    }
}