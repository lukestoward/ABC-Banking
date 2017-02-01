using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABC_Banking.Core.DataAccess;
using ABC_Banking.Core.Models.Transactions;

namespace ABC_Banking.Core
{
    public class StatementManager : IDisposable
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public async Task<List<ITransaction>> GetAccountTransactions(string accNumber, string sortCode,
            DateTime dateFrom, DateTime dateTo, int maxCount)
        {
            try
            {
                //1. Find account
                var account = await _unitOfWork.BankAccountRepository.GetFirst(x => x.AccountNumber == accNumber && x.SortCode == sortCode);

                //2. Load transaction variations
                var deposits = await _unitOfWork.DepositTransactionRepository.Get(x => x.BankAccountId == account.Id);
                var withdrawals = await _unitOfWork.WithdrawTransactionRepository.Get(x => x.BankAccountId == account.Id);
                var transfers = await _unitOfWork.TransferTransactionRepository.Get(x => x.BankAccountId == account.Id);

                //Set withdraw and transfer values to negative amounts
                foreach (var w in withdrawals)
                {
                    w.TransactionAmount = (0 - w.TransactionAmount);
                }

                foreach (var t in transfers)
                {
                    t.TransactionAmount = (0 - t.TransactionAmount);
                }

                List<ITransaction> transactions = new List<ITransaction>();

                transactions.AddRange(deposits);
                transactions.AddRange(withdrawals);
                transactions.AddRange(transfers);

                transactions = transactions.OrderByDescending(x => x.DateRequested).ToList();

                return transactions;
            }
            catch (Exception ex)
            {
                //Log error
                throw;
            }

        }


        #region DISPOSE 

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

        #endregion
    }
}
