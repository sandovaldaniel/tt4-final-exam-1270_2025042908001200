namespace Backend.Data
{
    using Microsoft.EntityFrameworkCore;
    using Backend.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

       
        public DbSet<ExpenseItem> Expenses { get; set; }
    }
}