using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ABC_Banking.Core.Models.BankAccounts;

namespace ABC_Banking.Core.Models.Transactions
{
    public class TransferTransaction : ITransaction
    {
        public TransferTransaction()
        {
            DateRequested = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateRequested { get; set; }

        [Required]
        public Guid BankAccountId { get; set; }

        [ForeignKey(nameof(BankAccountId))]
        public BankAccount BankAccount { get; set; }
        
        [Required]
        public decimal TransactionAmount { get; set; }

        [Required, MaxLength(18)]
        public string TransferReference { get; set; }

        [Required]
        public DateTime DateOfPayment { get; set; }

        #region BENEFACTOR DETAILS
        
        [Required, MaxLength(6), MinLength(6)]
        public string BenefactorSortCode { get; set; }

        [Required, MaxLength(8), MinLength(8)]
        public string BenefactorAccountNumber { get; set; }

        #endregion

        #region RECIPIENT DETAILS

        [Required]
        public string RecipientFullName { get; set; }

        [Required, MaxLength(6), MinLength(6)]
        public string RecipientSortCode { get; set; }

        [Required, MaxLength(8), MinLength(8)]
        public string RecipientAccountNumber { get; set; }

        #endregion

        public bool Processed { get; set; }

        public bool IsValid()
        {
            //Validates the class against its data annotations
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(this, context, results);

            return isValid;
        }
    }
}
