using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC_Banking.Core.Models.BankAccounts
{
    public class DailyCurrentAccount : BankAccount
    {
        public DailyCurrentAccount()
        {
            //Current Account default values
            OverdraftLimit = 50.00m;
            OverdraftChargeRate = 1.50m;
            CheckBookIssued = false;
            DailyWithdrawalLimit = 500.00m;
            Type = EnumBankAccountType.CurrentAccount;
        }

        [Required, Column(TypeName = "money")]
        public decimal OverdraftLimit { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal OverdraftChargeRate { get; set; }

        [Required]
        public bool CheckBookIssued { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal DailyWithdrawalLimit { get; set; }
    }
}
