using Domain.Models;
using Microsoft.EntityFrameworkCore;
using PostDigitaliser.Server.Models;

namespace InfrastructureEF.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Receipts> Receipts { get; set; }
        public DbSet<ShipmentDetails> ShipmentDetails { get; set; }
        public DbSet<Images> Images { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between one Receipts and many ShipmentDetails
            // Receipts is the principal and ShipmentDetails is the dependent
            modelBuilder.Entity<Receipts>()
                .HasMany(r => r.ShipmentDetails)
                .WithOne(s => s.Receipts)
                .HasForeignKey(s => s.ReceiptId);

            // Configure the one-to-one relationship between one Images and one Receipts
            // Images is the principal and Receipts is the dependent
            modelBuilder.Entity<Images>()
                .HasOne(i => i.Receipts)
                .WithOne(r => r.Image)
                .HasForeignKey<Receipts>(r => r.ImageId)
                .IsRequired();
        }
    }
}
