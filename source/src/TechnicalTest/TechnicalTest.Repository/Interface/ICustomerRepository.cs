using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.GenericRepository;
using TechnicalTest.Model.Entity;

namespace TechnicalTest.Repository.Interface
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IList<Customer>> GetCustomers();
        Task<IList<Customer>> GetCustomersByName(string name);
        Task<Customer> GetCustomerByEmail(string email);
        Task<bool> BulkImportCustomerData(IList<Customer> customers);
    }
}
