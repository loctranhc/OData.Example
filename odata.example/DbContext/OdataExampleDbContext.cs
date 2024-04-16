using Microsoft.EntityFrameworkCore;
using odata.example.Entities;

namespace odata.example.DbContext
{
    public class OdataExampleDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "OdataExampleDb");
        }

        public DbSet<Person> Persons { get; set; }
    }
}