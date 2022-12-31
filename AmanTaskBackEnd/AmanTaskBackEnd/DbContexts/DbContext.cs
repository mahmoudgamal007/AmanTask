using AmanTaskBackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmanTaskBackEnd.AmanDBContexts
{
    public class AmanDbContext : DbContext
    {
        public AmanDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
