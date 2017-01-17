using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC_Banking.Core.Models.BankAccounts
{
    public class InstantSaverAccount : BankAccount
    {
        public InstantSaverAccount()
        {
            AccountName = "Instant Saver";
            MinimumDeposit = 1.00m;
            AccountCategory = "Instant Access";
            AllowedWithdrawals = 9999;
            NoticePeriod = 0;
            Type = EnumBankAccountType.SavingsAccount;
        }

        [Required]
        public string AccountName { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal MinimumDeposit { get; set; }

        [Required]
        public string AccountCategory { get; set; }

        [Required]
        public int AllowedWithdrawals { get; set; }

        [Required]
        public int NoticePeriod { get; set; }
    }
}
