using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ABC_Banking.Core.Models.BankCards;
using ABC_Banking.Core.Models.Transactions;

namespace ABC_Banking.Core.Models.BankAccounts
{
    public abstract class BankAccount
    {
        protected BankAccount()
        {
            Balance = 0.00m;
            DateOpened = DateTime.UtcNow;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public EnumBankAccountType Type { get; protected set; }

        [Required]
        public DateTime DateOpened { get; set; }

        [Required, MinLength(8), MaxLength(8)]
        [Index("IX_AccountNumber", 1, IsUnique = true)]
        public string AccountNumber { get; set; }

        [Required, MinLength(6), MaxLength(6)]
        public string SortCode { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal Balance { get; set; }

        [Required]
        public float InterestRate { get; set; }

        public virtual ICollection<BankCard> Cards { get; set; }
        
        public virtual ICollection<ITransaction> Transactions { get; set; }
    }
}