using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ABC_Banking.Core;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Models.Transactions;
using ABC_Banking.Core.Validation;
using ABC_Banking.Services.BankTransfer.Models;

namespace ABC_Banking.Services.BankTransfer
{
    internal class TransferHandler
    {
        private ValidationResult _vResult = new ValidationResult();

        internal async Task<IValidationResult> Process(TransferRequest request)
        {
            //1. Authenticate the user making the request

            /*
             * You would need a way of checking that the source of the request
             * is being made by an authenticated/authorised customer, so that
             * no fake transfer requests could be completed. This is where that check
             * could be made.             
             */

            //2. Check the request values
            bool valid = CheckRequestValues(request);

            if (valid == false)
                return _vResult;

            //3. Check for a valid recipient bank account
            BankAccount bankAccount = await CheckForBankAccount(request);

            if (bankAccount == null)
            {
                _vResult.AddError("Benefactor bank account not found.");
                return _vResult;
            }
            
            //4. Check the account has the funds to make the transfer
            if (bankAccount.Balance < request.TransferAmount)
            {
                _vResult.AddError("Benefactor has insufficient funds");
                return _vResult;
            }

            //5. Finalise the transaction
            bool complete = await FinaliseTransfer(request, bankAccount);

            return _vResult;
        }
        

        private bool CheckRequestValues(TransferRequest request)
        {
            bool valid = true;

            if (request.TransferAmount <= 0.00m)
            {
                _vResult.AddError("Transfer amount must be a positive value.");
                valid = false;
            }


            //Check the date of payment is within 31 days
            var today = DateTime.UtcNow.Date;
            var latestDate = today.AddDays(31);

            if (request.DateOfPayment < today)
            {
                _vResult.AddError("The payment date must be a present or future date.");
                valid = false;
            }

            if (request.DateOfPayment > latestDate)
            {
                _vResult.AddError("The payment date must be within 31 days from todays date");
                valid = false;
            }

            //Additional checks could include regex tests 
            //on sort code or account number etc.

            return valid;
        }


        private async Task<BankAccount> CheckForBankAccount(TransferRequest request)
        {
            try
            {
                using (var manager = new CustomerAccountManager())
                {
                    BankAccount acc = await manager.GetCustomerAccount(
                    request.BenefactorAccountNumber,
                    request.BenefactorSortCode);

                    return acc;
                }
            }
            catch (Exception ex)
            {
                _vResult.AddException(ex);
                return null;
            }
        }

        private async Task<bool> FinaliseTransfer(TransferRequest request, BankAccount account)
        {
            try
            {
                //Create the transaction instance
                TransferTransaction transaction = new TransferTransaction()
                {
                    BankAccountId = account.Id,
                    BenefactorAccountNumber = request.BenefactorAccountNumber,
                    BenefactorSortCode = request.BenefactorSortCode,
                    DateOfPayment = request.DateOfPayment,
                    RecipientAccountNumber = request.RecipientAccountNumber,
                    RecipientFullName = request.RecipientFullName,
                    RecipientSortCode = request.RecipientSortCode,
                    TransferAmount = request.TransferAmount,
                    TransferReference = request.TransferReference
                };

                //Check we have created a valid transaction object
                if (transaction.IsValid())
                {
                    //Pass the transaction to the core to handle
                    using (var transactionManager = new TransactionManager())
                    {
                        ValidationResult vResult = await transactionManager.FinaliseTransaction(transaction);

                        if (vResult.HasError())
                        {
                            //Generic failure error (should use a more informative error)
                            _vResult.AddError("Unable to process transaction at this time. Please try again later");
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _vResult.AddException(ex);
                return true;
            }
        }
    }
}