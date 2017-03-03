using Microsoft.EntityFrameworkCore;

namespace accounts.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        //For each table we want to work with, we create a DbSet property
        //Our model for this table doesn't have to have the same name, only our DbSet property's name must match the table.
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}