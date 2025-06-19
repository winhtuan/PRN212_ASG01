using FUMiniHotelSystem.BLL.IService;
using FUMiniHotelSystem.DAL.IRepository;
using FUMiniHotelSystem.DAL.Repository;
using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.BLL.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService()
        {
            _customerRepo = CustomerRepository.Instance;
        }

        public List<Customer> GetAll() => _customerRepo.GetAll();

        public Customer? GetById(int id) => _customerRepo.GetById(id);

        public Customer? GetByEmail(string email) => _customerRepo.GetByEmail(email);

        public void Add(Customer customer)
        {
            if (!string.IsNullOrWhiteSpace(customer.EmailAddress))
            {
                _customerRepo.Add(customer);
            }
        }

        public void Update(Customer customer)
        {
            if (customer.CustomerID > 0)
            {
                _customerRepo.Update(customer);
            }
        }

        public void Delete(int id)
        {
            _customerRepo.Delete(id);
        }

        public List<Customer> Search(string keyword)
        {
            var all = _customerRepo.GetAll();
            if (string.IsNullOrWhiteSpace(keyword)) return all;

            keyword = keyword.ToLower();
            return all.Where(c =>
                c.CustomerFullName.ToLower().Contains(keyword) ||
                c.EmailAddress.ToLower().Contains(keyword) ||
                c.Telephone.ToLower().Contains(keyword)
            ).ToList();
        }
    }
}