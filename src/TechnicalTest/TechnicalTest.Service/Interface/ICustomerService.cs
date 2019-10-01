using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalTest.Model.Entity;

namespace TechnicalTest.Service.Interface
{
    public interface ICustomerService
    {
        Task<IList<Customer>> GetCustomers();
        Task<IList<Customer>> GetCustomersByName(string name);
        Task<(bool, string)> ImportCustomersFromExcel(string path, int startColumn = 1);
    }
}
