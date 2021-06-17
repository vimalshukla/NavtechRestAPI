using Database;
using Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Customers;
using Services.Helper;
using Services.Enums;

namespace Services.Customers
{
    public class CustomerService : ICustomerService
    {
        readonly SimpleStoreEntities _db;
        public CustomerService()
        {
            this._db = new SimpleStoreEntities();
        }
        public bool CheckEmailExists(string email)
        {
            return _db.CustomerDetails.Any(x => x.Email == email);
        }

        public Customer CreateCustomer(Customer customer)
        {
            CustomerDetail customerDetails = ModelMapper.Map<CustomerDetail>(customer);
            customerDetails.StatusId = (int)StatusEnum.Active;
            customerDetails.CreatedDate =  customerDetails.ModifiedDate = DateTime.Now;
            customerDetails = _db.CustomerDetails.Add(customerDetails);
            _db.SaveChanges();
            return ModelMapper.Map<Customer>(customerDetails);
        }

        
        public Customer Get(int id)
        {
            return ModelMapper.Map<Customer>(_db.CustomerDetails.FirstOrDefault(x => x.ID == id));
        }
    }
}
