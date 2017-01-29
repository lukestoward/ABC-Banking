using System;
using ABC_Banking.Core.DataAccess;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Models.Transactions;
using ABC_Banking.Core.Validation;

namespace ABC_Banking.Core
{
    public class TransactionManager : IDisposable
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public IValidationResult FinaliseTransaction(ITransaction transaction)
        {
            try
            {
                ValidationResult vResult = new ValidationResult();

                /* Additional checks and business logic [here]
                 * to manage the saving of transactions. You could have
                 * logic that pushed the transaction to a message queue that
                 * would be processed by an observing system. Just like when a cheque takes
                 * a few days to clear, or when you deposit money, it may take a few hours
                 * to show up.
                 */

                //Check we have been passed an object
                if (transaction == null)
                {
                    vResult.AddException(new ArgumentNullException(nameof(transaction), "A transaction object must be provided"));
                    return vResult;
                }



                //Simply save the transaction to the relevant repository
                if (transaction is DepositTransaction)
                {
                    _unitOfWork.DepositTransactionRepository.Insert((DepositTransaction)transaction);
                }
                else if (transaction is WithdrawTransaction)
                {
                    ProcessWithdrawRequest((WithdrawTransaction) transaction);
                }

                _unitOfWork.Save();

                return vResult;
            }
            catch (Exception ex)
            {
                //Handle exception
                throw;
            }
            
        }


        private void ProcessWithdrawRequest(WithdrawTransaction transaction)
        {
            //1. Find Bank Account
            //2. Subtract amount
            //3. Insert transaction into database
            //4. Save changes



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
