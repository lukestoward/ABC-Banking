using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABC_Banking.Core.DataAccess;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Validation;

namespace ABC_Banking.Core
{
    public class CustomerAccountManager : IDisposable
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public async Task<BankAccount> GetCustomerAccount(string accountNumber, string sortCode)
        {
            //Find the bank account
            BankAccount bankAccount = await GetBankAccount(accountNumber, sortCode);

            return bankAccount;
        }

        /// <summary>
        /// Method will check the balance of an account against a desired minimum amount
        /// and will add an error only if the desired amount exceeds the account balance.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="sortCode"></param>
        /// <param name="desiredMoney"></param>
        /// <returns></returns>
        public async Task<ValidationResult> AccountHasFunds(string accountNumber, string sortCode,
            decimal desiredMoney)
        {
            ValidationResult vResult = new ValidationResult();

            try
            {
                BankAccount bankAccount = await GetBankAccount(accountNumber, sortCode);

                if (bankAccount == null)
                {
                    vResult.AddException(new KeyNotFoundException("Bank Account not be found"));
                    return vResult;
                }

                //Check balance
                var fundsAvailable = bankAccount.Balance >= desiredMoney;

                //Add an error if funds are insufficient
                if (fundsAvailable == false)
                {
                    vResult.AddError("Insufficient funds");
                }
                
                return vResult;
            }
            catch (Exception ex)
            {
                //Log Error
                vResult.AddException(ex);
                return vResult;
            }
        }


        private async Task<BankAccount> GetBankAccount(string accountNumber, string sortCode)
        {
            try
            {
                 return await _unitOfWork.BankAccountRepository.GetFirst(
                                    x => x.AccountNumber == accountNumber &&
                                         x.SortCode == sortCode);
            }
            catch (Exception ex)
            {
                //Log Error
                return null;
            }
           
        }

        private bool disposed = false;

        /// <summary>
        /// Handle the disposal of the unit of work obj
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
