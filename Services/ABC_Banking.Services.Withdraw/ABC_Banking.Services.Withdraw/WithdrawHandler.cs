using System;
using System.Threading.Tasks;
using ABC_Banking.Core.Security;
using ABC_Banking.Services.Withdraw.Models;
using ABC_Banking.Core.Validation;
using ABC_Banking.Core;
using ABC_Banking.Core.Models.Transactions;

namespace ABC_Banking.Services.Withdraw
{
    internal class WithdrawHandler
    {
        private readonly IValidationResult _result;

        public WithdrawHandler()
        {
            _result = new ValidationResult();
        }

        internal async Task<IValidationResult> Process(WithdrawRequest request)
        {
            //First check the authentication details
            bool authorised = await CheckAuthorisation(request);

            if (authorised == false)
                return _result;

            //Next, check account status, funds and availability
            bool canWithdraw = await CanWithdrawFunds(request);

            if (canWithdraw == false)
                return _result;

            //Now we can actually commit the transaction.
            await FinaliseWithdrawal(request);

            return _result;
        }

        /// <summary>
        /// Authorisation checks will are performed to check if the PIN entered is valid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<bool> CheckAuthorisation(WithdrawRequest request)
        {
            var valid = false;

            using (AuthorisationServices authorisor = new AuthorisationServices())
            {
                IValidationResult vResult = await authorisor.ValidatePin(request.CardNumber, request.Pin);

                if (vResult.HasError() == false)
                {
                    valid = true;
                }
                else
                {
                    //Grab the errors from the vResult and add them to the global _result errors
                    vResult.Errors.ForEach(_result.AddError);
                }
            }

            return valid;
        }

        private async Task<bool> CanWithdrawFunds(WithdrawRequest request)
        {
            bool valid = false;

            //HERE YOU CAN PERFORM ALL THE CHECKS NECCESSARY ON AN ACCOUNT TO
            //CONCLUDE WHETHER OR NOT A REQUEST CAN BE ACCEPTED...

            //Check if the funds are available
            using (CustomerAccountManager manager = new CustomerAccountManager())
            {
                IValidationResult vResult = await manager.AccountHasFunds(request.AccountNumber, 
                    request.SortCode, request.TotalCashValue);

                if (vResult.HasError() == false)
                {
                    // No errors/issues, funds are available
                    valid = true;
                    return valid;
                }
                else
                {
                    //Grab the errors from the vResult and add them to the global _result errors
                    vResult.Errors.ForEach(_result.AddError);
                    vResult.Exceptions.ForEach(_result.AddException);
                }
            }

            return valid;
        }

        /// <summary>
        /// Creates the transaction object and commits to the datastore
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Task FinaliseWithdrawal(WithdrawRequest request)
        {
            try
            {
                //Create the transaction instance
                WithdrawTransaction transaction = new WithdrawTransaction(request.TotalCashValue)
                {
                    AccountNumber = request.AccountNumber,
                    SortCode = request.SortCode
                };

                //Check we have created a valid transaction object
                if (transaction.IsValid())
                {
                    //Pass the transaction to the core to handle
                    using (var transactionManager = new TransactionManager())
                    {
                        var success = transactionManager.FinaliseTransaction(transaction);

                        if (success == false)
                        {
                            //Generic failure error (should use a more informative error)
                            _result.AddError("Unable to process transaction at this time. Please try again later");
                        }

                        return success;
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}