using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Interfaces
{
    public interface ICustomerService
    {
        Task<IList<Customer>> GetCustomers();
        Task<Customer> PutInsertCustomer(Customer customer);
    }
}
