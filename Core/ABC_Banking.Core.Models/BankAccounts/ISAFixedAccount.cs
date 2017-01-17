using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Banking.Core.Models.BankAccounts
{
    public class ISAFixedAccount : BankAccount
    { 
        public ISAFixedAccount()
        {
            AccountName = "FI";
            MinimumDeposit = 1.00m;
            AccountCategory = "Fixed ISA";
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

        [Required]
        public int DurationMonths { get; set; }
    }
}
