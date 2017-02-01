using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABC_Banking.Core.DataAccess;
using ABC_Banking.Core.Models.BankAccounts;
using ABC_Banking.Core.Models.Transactions;
using ABC_Banking.Core.Validation;

namespace ABC_Banking.Core
{
    public class TransactionManager : IDisposable
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public async Task<ValidationResult> FinaliseTransaction(ITransaction transaction)
        {
            ValidationResult vResult = new ValidationResult();

            try
            {
                //Check we have been passed an object
                if (transaction == null)
                {
                    vResult.AddException(new ArgumentNullException(nameof(transaction),
                        "A transaction object must be provided"));
                    return vResult;
                }

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
                    vResult = await ProcessDepositRequest((DepositTransaction) transaction);
                }
                else if (transaction is WithdrawTransaction)
                {
                    vResult = await ProcessWithdrawRequest((WithdrawTransaction) transaction);
                }
                else if (transaction is TransferTransaction)
                {
                    vResult = await ProcessBankTransfer((TransferTransaction) transaction);
                }
                else
                {
                    throw new NotImplementedException();
                }


                return vResult;
            }
            catch (Exception ex)
            {
                vResult.AddException(ex);
                return vResult;
            }

        }

        private async Task<ValidationResult> ProcessDepositRequest(DepositTransaction transaction)
        {
            try
            {
                _unitOfWork.DepositTransactionRepository.Insert(transaction);
                _unitOfWork.Save();

                //Success
                return new ValidationResult();
            }
            catch (Exception ex)
            {
                var vResult = new ValidationResult();
                vResult.AddException(ex);
                return vResult;
            }
        }

        private async Task<ValidationResult> ProcessWithdrawRequest(WithdrawTransaction transaction)
        {
            var vResult = new ValidationResult();

            try
            {
                //1. Find Bank Account
                BankAccount account =
                    await _unitOfWork.BankAccountRepository.GetFirst(x => x.AccountNumber == transaction.AccountNumber);

                if (account == null)
                {
                    vResult.AddException(new KeyNotFoundException("Bank Account could not be found"));
                    return vResult;
                }

                //2. Subtract amount
                account.Balance -= transaction.TransactionAmount;

                //2.1 Add bank account Id as foreign key to transaction
                transaction.BankAccountId = account.Id;

                _unitOfWork.BankAccountRepository.Update(account);

                //3. Insert transaction into database
                _unitOfWork.WithdrawTransactionRepository.Insert(transaction);

                //4. Save changes
                _unitOfWork.Save();

                return vResult;
            }
            catch (Exception ex)
            {
                vResult.AddException(ex);
                return vResult;
            }

        }

        private async Task<ValidationResult> ProcessBankTransfer(TransferTransaction transaction)
        {
            ValidationResult vResult = new ValidationResult();

            try
            {
                /*
                 * Realistically a bank transfer request can take some time to be completed,
                 * or may have a specific date to complete the transfer as such, to simulate 
                 * that behaviour we will not deduct the funds from the
                 * customers account. Instead we would save the transaction to the database, 
                 * notify a sub system that would process the transaction. However this 
                 * subsystem would be a seperate application and out of scope here.
                 */

                //We simple insert the transaction to be processed later.
                _unitOfWork.TransferTransactionRepository.Insert(transaction);
                await _unitOfWork.SaveAsync();

                //Success
                return new ValidationResult();
            }
            catch (Exception ex)
            {

                vResult.AddException(ex);
                return vResult;
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
