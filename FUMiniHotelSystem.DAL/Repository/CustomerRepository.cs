using FUMiniHotelSystem.DAL.IRepository;
using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.DAL.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private static CustomerRepository _instance;
        private List<Customer> _customers;

        public static CustomerRepository Instance => _instance ??= new CustomerRepository();

        private CustomerRepository()
        {
            _customers = new List<Customer>
            {
                new Customer { CustomerID = 1, CustomerFullName = "John Doe", Telephone = "0123456789", EmailAddress = "john@example.com", CustomerBirthday = new DateTime(1990, 5, 12), CustomerStatus = 1, Password = "123456" },
                new Customer { CustomerID = 2, CustomerFullName = "Alice Smith", Telephone = "0987654321", EmailAddress = "alice@hotel.com", CustomerBirthday = new DateTime(1992, 3, 20), CustomerStatus = 1, Password = "password1" },
                new Customer { CustomerID = 3, CustomerFullName = "Bob Johnson", Telephone = "0911222333", EmailAddress = "bob@domain.com", CustomerBirthday = new DateTime(1985, 8, 10), CustomerStatus = 1, Password = "qwerty" },
                new Customer { CustomerID = 4, CustomerFullName = "Catherine Lee", Telephone = "0922333444", EmailAddress = "catherine@guest.com", CustomerBirthday = new DateTime(1998, 7, 25), CustomerStatus = 1, Password = "abc123" },
                new Customer { CustomerID = 5, CustomerFullName = "David Brown", Telephone = "0933444555", EmailAddress = "davidb@hotel.com", CustomerBirthday = new DateTime(1991, 2, 5), CustomerStatus = 1, Password = "davidpass" },
                new Customer { CustomerID = 6, CustomerFullName = "Emma Watson", Telephone = "0944555666", EmailAddress = "emma@hotelmail.com", CustomerBirthday = new DateTime(1994, 11, 11), CustomerStatus = 1, Password = "emma2024" },
                new Customer { CustomerID = 7, CustomerFullName = "Frank Martin", Telephone = "0955666777", EmailAddress = "frank@booking.com", CustomerBirthday = new DateTime(1988, 9, 9), CustomerStatus = 1, Password = "martin88" },
                new Customer { CustomerID = 8, CustomerFullName = "Grace Kim", Telephone = "0966777888", EmailAddress = "gracekim@mail.com", CustomerBirthday = new DateTime(1996, 6, 30), CustomerStatus = 1, Password = "grace96" },
                new Customer { CustomerID = 9, CustomerFullName = "Henry Ford", Telephone = "0977888999", EmailAddress = "henry@travel.com", CustomerBirthday = new DateTime(1993, 4, 4), CustomerStatus = 1, Password = "ford93" },
                new Customer { CustomerID = 10, CustomerFullName = "Isabella Green", Telephone = "0988999000", EmailAddress = "isabella@client.com", CustomerBirthday = new DateTime(1999, 12, 1), CustomerStatus = 1, Password = "greenpass" }
            };
        }

        public List<Customer> GetAll() => _customers;

        public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.CustomerID == id);

        public Customer? GetByEmail(string email) => _customers.FirstOrDefault(c => c.EmailAddress == email);

        public void Add(Customer customer)
        {
            customer.CustomerID = _customers.Any() ? _customers.Max(c => c.CustomerID) + 1 : 1;
            _customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            var existing = GetById(customer.CustomerID);
            if (existing != null)
            {
                existing.CustomerFullName = customer.CustomerFullName;
                existing.EmailAddress = customer.EmailAddress;
                existing.Telephone = customer.Telephone;
                existing.CustomerBirthday = customer.CustomerBirthday;
                existing.CustomerStatus = customer.CustomerStatus;
                existing.Password = customer.Password;
            }
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                customer.CustomerStatus = 2; // Mark as deleted
            }
        }
    }
}