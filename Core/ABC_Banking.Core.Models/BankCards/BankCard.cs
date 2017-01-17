using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ABC_Banking.Core.Models.BankAccounts;

namespace ABC_Banking.Core.Models.BankCards
{
    public abstract class BankCard
    {

        [Key]
        public int Id { get; set; }

        [Required, MinLength(16), MaxLength(16)]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string NameOnCard { get; set; }

        [Required]
        public string SortCode { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required, MinLength(4), MaxLength(4)]
        public int Pin { get; set; }

        [Required]
        public EnumBankCardType CardType { get; set; }

        [Required]
        public Guid BankAccountId { get; set; }

        [ForeignKey(nameof(BankAccountId))]
        public BankAccount BankAccount { get; set; }
    }
}
