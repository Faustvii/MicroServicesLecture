using Microsoft.EntityFrameworkCore;

namespace MicroServicesLecture.Model
{
    public class MicroServicesLectureContext : DbContext
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public MicroServicesLectureContext(DbContextOptions<MicroServicesLectureContext> options) : base(options)
        {

        }

    }
}