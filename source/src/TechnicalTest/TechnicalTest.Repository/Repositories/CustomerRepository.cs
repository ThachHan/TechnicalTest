using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.GenericRepository;
using TechnicalTest.Model.DbManager;
using TechnicalTest.Model.Entity;
using TechnicalTest.Repository.Interface;

namespace TechnicalTest.Repositoriy
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        #region field and constructor
        private readonly EntityCoreContext _context;
        public CustomerRepository(EntityCoreContext context)
            : base(context)
        {
            _context = context;
        }
        #endregion       

        
        public async Task<IList<Customer>> GetCustomers()
        {
             return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(n => n.Email == email);
        }

        public async Task<IList<Customer>> GetCustomersByName(string name)
        {
            return await _context.Customers.AsNoTracking().Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name)).ToListAsync();
        }

        public async Task<bool> BulkImportCustomerData(IList<Customer> customers)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.BulkInsert(customers, new BulkConfig { PreserveInsertOrder = true, SetOutputIdentity = true, BatchSize = 1000 });
                    await _context.BulkInsertOrUpdateAsync(customers);
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }

            }
        }
    }
}
