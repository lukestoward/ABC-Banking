using System;
using System.Threading.Tasks;
using ABC_Banking.Core.DataAccess;
using ABC_Banking.Core.Models.BankAccounts;

namespace ABC_Banking.Core
{
    public class CustomerAccountManager : IDisposable
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public async Task<BankAccount> GetCustomerAccount(string accountNumber, string sortCode)
        {
            //Find the bank account
            BankAccount bankAccount = await _unitOfWork.BankAccountRepository.GetFirst(
                acc => acc.AccountNumber == accountNumber &&
                acc.SortCode == sortCode);

            return bankAccount;
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
