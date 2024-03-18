using Microsoft.EntityFrameworkCore;

namespace BankingApp.Models.DatabaseModels
{
    public class BankingAppDbContext : DbContext
    {
        public BankingAppDbContext(DbContextOptions<BankingAppDbContext> options) : base(options)
        {
           
        }

        public DbSet<Transaction>  Transaction { get; set; }    
        public DbSet<Account> Account { get; set; }
        public DbSet<TransactionStatus> TransactionStatus { get; set; }
        public DbSet<Currency> Currency { get; set; }

    }
}
