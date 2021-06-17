using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Customer;

namespace Services.Customers
{
    public interface ICustomerService
    {
        /// <summary>
        /// Creates Customer based on customer model.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Returns true if created else false</returns>
        Customer CreateCustomer(Customer customer);

        /// <summary>
        /// Checks if email already exist or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Returns true if already exist else false</returns>
        bool CheckEmailExists(string email);

        /// <summary>
        /// Get Customer details by customer ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer Model</returns>
        Customer Get(int id);
    }
}
