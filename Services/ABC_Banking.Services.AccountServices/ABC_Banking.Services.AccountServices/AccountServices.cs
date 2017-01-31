using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ABC_Banking.Core;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Validation;
using ABC_Banking.Services.AccountServices.Models;

namespace ABC_Banking.Services.AccountServices
{
    public class AccountServices
    {
        public IValidationResult CreateBankAccount(NewBankAccount model)
        {
            ValidationResult vResult = new ValidationResult();


            return vResult;
        }

        public IValidationResult CloseBankAccount(BankAccountDTO model)
        {
            ValidationResult vResult = new ValidationResult();

            /*
             * In order to close a bank account a number of checks must
             * be satisfied, for example, the customer must not be in their
             * overdraft and have outstanding debt. In this instance the account
             * must have a balance of zero.
             */

            //1. First check the an account exists
            //2. Peform pre-closure checks
            //3. Archive account
            //4. Save changes

            /*
             * Other systems may need to be notified of the closure
             * to peform set procedures, like for example send out a closure 
             * confirmation letter to the customer.
             */

            return vResult;
        }

        public async Task<AccountBalance> GetAccountBalance(BankAccountDTO model)
        {
            //1. Find the bank account
            var bankAccount = await GetBankAccount(model.AccountNumber, model.SortCode);

            if(bankAccount == null)
                return null;

            //2. Return account balance
            AccountBalance balance = new AccountBalance
            {
                Balance = bankAccount.Balance
            };

            return balance;
        }

        private async Task<BankAccount> GetBankAccount(string accountNumber, string sortCode)
        {
            try
            {
                using (CustomerAccountManager manager = new CustomerAccountManager())
                {
                    return await manager.GetCustomerAccount(accountNumber, sortCode);
                }
            }
            catch (Exception ex)
            {
                //Log error...
                return null;
            }
        }
    }
}