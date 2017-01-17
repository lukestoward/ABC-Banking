using System;
using ABC_Banking.Core.DataAccess;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Models.Transactions;

namespace ABC_Banking.Core
{
    public class TransactionManager : IDisposable
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public bool FinaliseTransaction(BankAccount account, ITransaction transaction)
        {
            try
            {
                /* Additional checks and business logic [here]
                 * to manage the saving of transactions. You could have
                 * logic that pushed the transaction to a message queue that
                 * would be processed by an observing system. Just like when a cheque takes
                 * a few days to clear, or when you deposit money, it may take a few hours
                 * to show up.
                 */

                //Simply save the transaction to the relevant repository
                if (transaction is DepositTransaction)
                {
                    _unitOfWork.DepositTransactionRepository.Insert((DepositTransaction)transaction);
                }
                //else if (transaction is WithdrawalTransaction)
                //{
                //    _unitOfWork.WithdrawalTransactionRepository.Insert((WithdrawalTransaction)transaction);
                //}

                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                //Handle exception
                throw;
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
