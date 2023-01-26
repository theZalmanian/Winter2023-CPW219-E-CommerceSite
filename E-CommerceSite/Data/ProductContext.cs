using E_CommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSite.Data
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
