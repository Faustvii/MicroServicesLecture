using Microsoft.EntityFrameworkCore;

namespace SU.Model
{
    public class BasketContext : DbContext
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public BasketContext(DbContextOptions<BasketContext> options) : base(options)
        {

        }

    }
}