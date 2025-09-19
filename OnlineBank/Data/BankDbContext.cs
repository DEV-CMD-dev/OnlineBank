using OnlineBank.Data.Entities;
using Microsoft.EntityFrameworkCore;
using OnlineBank.Data.Classes;

namespace OnlineBank.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext() { }

        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.CVV)
                    .HasConversion(new CardCVVConverter())
                    .HasMaxLength(3)
                    .IsRequired();
            });

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.FromCard)
                .WithMany(c => c.SentTransactions)
                .HasForeignKey(t => t.FromCardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ToCard)
                .WithMany(c => c.ReceivedTransactions)
                .HasForeignKey(t => t.ToCardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Card>()
                .Property(c => c.Balance)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);
        }
    }
}
