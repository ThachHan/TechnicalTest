using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTest.Model.Entity;
using TechnicalTest.Repository.Interface;
using TechnicalTest.Service.Interface;

namespace TechnicalTest.Service
{
    public class CustomerService : ICustomerService
    {
        #region Field and constructor
        private readonly ICustomerRepository _customerRepository;

        #endregion
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<IList<Customer>> GetCustomers()
        {
            return await _customerRepository.GetCustomers();
        }

        public async Task<IList<Customer>> GetCustomersByName(string name)
        {
            return await _customerRepository.GetCustomersByName(name);
        }

        public async Task<(bool, string)> ImportCustomersFromExcel(string path, int startColumn = 1)
        {
            var customers = new List<Customer>();
            using (var excelPackage = new OfficeOpenXml.ExcelPackage())
            {
                using (var fileStream = File.OpenRead(path))
                {
                    excelPackage.Load(fileStream);
                }
                var workSheet = excelPackage.Workbook.Worksheets.First();

                for (int rowNum = startColumn; rowNum <= workSheet.Dimension.End.Row; rowNum++)
                {
                    var customer = new Customer()
                    {
                        FirstName = workSheet.Cells[rowNum, 1].Value?.ToString() ?? string.Empty,
                        LastName = workSheet.Cells[rowNum, 2].Value?.ToString() ?? string.Empty,
                        CompanyName = workSheet.Cells[rowNum, 3].Value?.ToString() ?? string.Empty,
                        Address = workSheet.Cells[rowNum, 4].Value?.ToString() ?? string.Empty,
                        City = workSheet.Cells[rowNum, 5].Value?.ToString() ?? string.Empty,
                        State = workSheet.Cells[rowNum, 6].Value?.ToString() ?? string.Empty,
                        Post = workSheet.Cells[rowNum, 7].Value?.ToString() ?? string.Empty,
                        Phone1 = workSheet.Cells[rowNum, 8].Value?.ToString() ?? string.Empty,
                        Phone2 = workSheet.Cells[rowNum, 9].Value?.ToString() ?? string.Empty,
                        Email = workSheet.Cells[rowNum, 10].Value?.ToString() ?? string.Empty,
                        Web = workSheet.Cells[rowNum, 11].Value?.ToString() ?? string.Empty
                    };
                
                    if (!string.IsNullOrEmpty(customer.Email))
                    {
                        var existedCustomer = await _customerRepository.GetCustomerByEmail(customer.Email);
                        if(existedCustomer != null)
                        {
                            if(customers.Any(uc => uc.Email == customer.Email))
                            {
                                return (false, string.Format("Email {0} has duplicated records. Please check the data again.", customer.Email));
                            }
                            else
                            {
                                existedCustomer = UpdatePropertyCustomer(existedCustomer, customer);
                                customers.Add(existedCustomer);
                            }
                        }
                        else{
                            customers.Add(customer);
                        }
                    }
                }
            }
            
            return customers.Any() ? (await _customerRepository.BulkImportCustomerData(customers),string.Empty) : (true,string.Empty);
        }

        private Customer UpdatePropertyCustomer(Customer existedCustomer, Customer currentCustomer)
        {
            existedCustomer.FirstName = currentCustomer.FirstName;
            existedCustomer.LastName = currentCustomer.LastName;
            existedCustomer.CompanyName = currentCustomer.CompanyName;
            existedCustomer.Address = currentCustomer.Address;
            existedCustomer.City = currentCustomer.City;
            existedCustomer.State = currentCustomer.State;
            existedCustomer.Post = currentCustomer.Post;
            existedCustomer.Phone1 = currentCustomer.Phone1;
            existedCustomer.Phone2 = currentCustomer.Phone2;
            existedCustomer.Web = currentCustomer.Web;
            return existedCustomer;
        }
    }
}
