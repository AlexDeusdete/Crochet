using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Crochet.Services.API
{
    public class CustomerService : ApiBase, ICustomerService
    {
        public CustomerService(IApi api):base(api) {}
        public async Task<IList<Customer>> GetCustomers()
        {
            return await API.GetCustomers();
        }

        public async Task<Customer> PutInsertCustomer(Customer customer)
        {
            if(customer.Id == 0)
                return await API.PostCustomer(customer);
            else
                return await API.PutCustomer(customer.Id, customer);
        }
    }
}
