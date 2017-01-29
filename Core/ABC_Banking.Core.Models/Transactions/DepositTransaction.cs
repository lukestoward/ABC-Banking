using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using ABC_Banking.Core.Models.BankAccounts;

namespace ABC_Banking.Core.Models.Transactions
{
    public class DepositTransaction : ITransaction
    {
        public DepositTransaction()
        {
            CashValue = 0.00m;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime DateRequested { get; set; }

        [Required]
        public Guid BankAccountId { get; set; }

        [Required]
        public decimal CashValue { get; set; }


        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
