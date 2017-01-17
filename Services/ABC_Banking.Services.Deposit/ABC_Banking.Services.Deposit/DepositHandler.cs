using System;
using System.Linq;
using System.Threading.Tasks;
using ABC_Banking.Services.Deposit.Models;
using ABC_Banking.Core;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Models.Transactions;
using ABC_Banking.Services.Deposit.Validation;

namespace ABC_Banking.Services.Deposit
{
    /// <summary>
    /// Logic for processing the deposit request
    /// </summary>
    public class DepositHandler
    {
        private readonly IValidationResult _result = new ValidationResult();

        public async Task<IValidationResult> Process(DepositRequest deposit)
        {
            //First find the bank account
            BankAccount acc = await GetCustomerBankAccount(deposit);

            if (acc == null)
            {
                _result.AddError($"Customer account [{deposit.AccountNumber}] could not be found");
                return _result;
            }

            //Next check the monetary values
            var valid = CheckDepositValues(deposit);

            if (valid == false)
                return _result;

            /* Addtional Service level logic [here]
             * to handle any of deposit transaction specific logic,
             * before finally passing the request to the core dll logic
             * to save and do everything else neccessary.
             */

            //Next create a transaction object
            var success = CreateTransaction(acc, deposit);

            if (!success)
                return _result;          

            //Finished processing, return success
            return _result;
        }

        private async Task<BankAccount> GetCustomerBankAccount(DepositRequest deposit)
        {
            try
            {
                using (var manager = new CustomerAccountManager())
                {
                    BankAccount acc = await manager.GetCustomerAccount(
                    deposit.AccountNumber,
                    deposit.SortCode);

                    return acc;
                }
            }
            catch (Exception ex)
            {
                //Log error
                _result.AddError(ex.Message);
                return null;
            }

        }

        private bool CheckDepositValues(DepositRequest deposit)
        {
            bool valid = true;

            //Positive value checks
            if (deposit.TotalCashValue <= 0.00m)
            {
                _result.AddError(nameof(deposit.TotalCashValue) + " must be a positive decimal value");
                valid = false;
            }

            //Any cheques?
            if (deposit.Cheques.Any())
            {
                //Check each cheque value is positive
                foreach (var cheque in deposit.Cheques)
                {
                    if (cheque.Value <= 0.00m)
                    {
                        _result.AddError($"Cheque {cheque.ChequeNumber} does not have a positive decimal value");
                        valid = false;
                    }
                }
            }

            return valid;
        }

        private bool CreateTransaction(BankAccount account, DepositRequest deposit)
        {
            try
            {
                //Create the transaction instance
                DepositTransaction transaction = new DepositTransaction
                {
                    BankAccountId = account.Id,
                    DateRequested = deposit.DateRequested,
                    CashValue = deposit.TotalCashValue
                };

                //Pass the transaction to the core to handle
                using (var transactionManager = new TransactionManager())
                {
                    var success = transactionManager.FinaliseTransaction(account, transaction);

                    if (success == false)
                    {
                        //Generic failure error (should use a more informative error)
                        _result.AddError("Unable to process transaction at this time. Please try again later");
                    }

                    return success;
                }
            }
            catch (Exception ex)
            {
                _result.AddError(ex.Message);
                return false;
            }
        }
    }
}