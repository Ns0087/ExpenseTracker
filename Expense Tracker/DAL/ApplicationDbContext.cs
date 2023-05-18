using Expense_Tracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here..
            modelBuilder.Entity<User>().HasKey("UserId");
        }
    }
}
