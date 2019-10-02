using Microsoft.EntityFrameworkCore;
using TechnicalTest.Model.Entity;

namespace TechnicalTest.Model.DbManager
{
    public partial class EntityCoreContext : DbContext
    {
        public EntityCoreContext(DbContextOptions<EntityCoreContext> options)
            : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }      
    }
}
