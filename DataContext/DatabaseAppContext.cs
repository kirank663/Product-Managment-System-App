using Microsoft.EntityFrameworkCore;
using ProductManagmentApp.Models;
using ProductManagmentApp.ViewModel;

namespace ProductManagmentApp.DataContext
{
    public class DatabaseAppContext:DbContext
    {
        public DatabaseAppContext(DbContextOptions<DatabaseAppContext> option):base(option) { 
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        //public DbSet<ProductManagmentApp.ViewModel.ProductViewModel> ProductViewModel { get; set; }
    }
}
