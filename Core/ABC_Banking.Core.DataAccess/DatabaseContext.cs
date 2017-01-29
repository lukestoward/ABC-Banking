using System.Data.Entity;
using ABC_Banking.Core.Models;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Models.BankCards;
using ABC_Banking.Core.Models.Transactions;

namespace ABC_Banking.Core.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() 
            : base("Data Source=.;Initial Catalog=ABC_Banking;Integrated Security=True")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Table Per Type (TPT) stratergy used to handle the bank account inheritance db mapping

            //Current Account types
            modelBuilder.Entity<DailyCurrentAccount>().ToTable("DailyCurrentAccounts");

            //Saving Account types
            modelBuilder.Entity<RegularSaverAccount>().ToTable("RegularSaverAccounts");
            modelBuilder.Entity<InstantSaverAccount>().ToTable("InstantSaverAccounts");
            modelBuilder.Entity<ISAFixedAccount>().ToTable("ISAFixedAccounts");

            //Transaction types
            modelBuilder.Entity<DepositTransaction>().ToTable("DepositTransactions");
            modelBuilder.Entity<WithdrawTransaction>().ToTable("WithdrawTransactions");
            modelBuilder.Entity<TransferTransaction>().ToTable("BankTransferTransactions");

            //Bank Card types
            modelBuilder.Entity<DebitCard>().ToTable("DebitCards");
            modelBuilder.Entity<CreditCard>().ToTable("CreditCards");

        }

        // Database sets
        // --- here ---
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //Bank Account Variations
        public DbSet<DailyCurrentAccount> DailyCurrentAccounts  { get; set; }
        public DbSet<RegularSaverAccount> RegularSaverAccounts  { get; set; }
        public DbSet<InstantSaverAccount> InstantSaverAccounts { get; set; }
        public DbSet<ISAFixedAccount> ISAFixedAccounts { get; set; }
        
        //Bank card variations
        public DbSet<DebitCard> DebitCards { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }


    }
}
