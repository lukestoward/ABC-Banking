using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using ABC_Banking.Core.Models.BankAccounts;

namespace ABC_Banking.Core.Models.Transactions
{
    public class WithdrawTransaction : ITransaction
    {
        public WithdrawTransaction(decimal requestedAmount)
        {
            CashValue = requestedAmount;
            DateRequested = DateTime.UtcNow;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime DateRequested { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string SortCode { get; set; }

        [Required]
        public Guid BankAccountId { get; set; }

        [ForeignKey(nameof(BankAccountId))]
        public BankAccount BankAccount { get; set; }

        [Required]
        public decimal CashValue { get; set; }


        public bool IsValid()
        {
            // Check that the value is positive
            if (CashValue <= 0.00m)
            {
                return false;
            }

            //Check withdraw amount is NOT greater than balance
            if (CashValue <= BankAccount?.Balance)
                return false;

            //Transaction must be valid
            return true;
        }
    }
}
