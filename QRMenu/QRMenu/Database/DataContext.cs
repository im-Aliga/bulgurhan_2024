using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QRMenu.Database.Models;

namespace QRMenu.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
          : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }




    }
}
