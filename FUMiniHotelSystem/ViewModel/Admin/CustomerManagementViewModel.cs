using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FUMiniHotelSystem.BLL.Service;
using FUMiniHotelSystem.Models;
using FUMiniHotelSystem.Utils;

namespace FUMiniHotelSystem.ViewModel.Admin
{
    public class CustomerManagementViewModel : ViewModelBase
    {
        private readonly CustomerService _customerService;

        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;
        private string _searchText;

        public CustomerManagementViewModel()
        {
            _customerService = new CustomerService();
            _customers = new ObservableCollection<Customer>(_customerService.GetAll());

            SearchCommand = new RelayCommand(_ => Search());
            EditCommand = new RelayCommand(param =>
            {
                if (param is Customer customer)
                    EditCustomer(customer);
            }, param => param is Customer);

            DeleteCommand = new RelayCommand(_ => DeleteCustomer(), _ => CanEditOrDelete());
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty(ref _selectedCustomer, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ICommand SearchCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private void Search()
        {
            var keyword = SearchText?.ToLower() ?? string.Empty;

            var result = string.IsNullOrWhiteSpace(keyword)
                ? _customerService.GetAll()
                : _customerService.GetAll().Where(c =>
                    (!string.IsNullOrEmpty(c.CustomerFullName) && c.CustomerFullName.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(c.Telephone) && c.Telephone.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(c.EmailAddress) && c.EmailAddress.ToLower().Contains(keyword))
                ).OrderBy(c => c.CustomerFullName)
                .ToList();

            Customers = new ObservableCollection<Customer>(result);
        }

        public void AddCustomerFromDialog(Customer customer)
        {
            _customerService.Add(customer);
            Customers.Add(customer);
        }

        private void EditCustomer(Customer customer)
        {
            var dialog = new AdminController.EditCustomerDialog(customer)
            {
                Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)
            };

            if (dialog.ShowDialog() == true)
            {
                var updated = dialog.UpdatedCustomer;
                _customerService.Update(updated);

                var index = Customers.IndexOf(Customers.FirstOrDefault(c => c.CustomerID == updated.CustomerID));
                if (index >= 0)
                {
                    Customers[index] = updated;
                }
            }
        }

        private void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                _customerService.Delete(SelectedCustomer.CustomerID);
                Customers.Remove(SelectedCustomer);
            }
        }

        private bool CanEditOrDelete()
        {
            return SelectedCustomer != null;
        }

        private void ReloadCustomers()
        {
            Customers = new ObservableCollection<Customer>(_customerService.GetAll());
        }
    }
}