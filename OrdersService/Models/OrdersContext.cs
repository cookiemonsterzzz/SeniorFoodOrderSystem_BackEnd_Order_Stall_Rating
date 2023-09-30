using Microsoft.EntityFrameworkCore;

namespace OrdersService.Models
{
    public partial class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options)
    : base(options)
        {
        }

        public DbSet<Orders> Orders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("Data Source=DESKTOP-QA7NAUN\\SQLEXPRESS;Initial Catalog=OrdersDb;Trusted_Connection=true;TrustServerCertificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("Orders");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
