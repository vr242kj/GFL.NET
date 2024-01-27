using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class YourDbContext : DbContext
    {
        public DbSet<ConfigurationModel> ConfigurationModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Встановіть з'єднання з вашою базою даних
            string connectionString = "Data Source=LAPTOP-CDCD3ABD\\BCDEMO;Initial Catalog=mydb;Integrated Security=true;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
