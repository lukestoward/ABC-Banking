using System.Threading.Tasks;
using ABC_Banking.Core.Security;
using ABC_Banking.Services.Withdraw.Models;
using ABC_Banking.Core.Validation;

namespace ABC_Banking.Services.Withdraw
{
    internal class WithdrawHandler
    {
        private IValidationResult _result;

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


            return _result;
        }

        private async Task<bool> CheckAuthorisation(WithdrawRequest request)
        {
            var valid = false;

            using (AuthorisationServices authorisor = new AuthorisationServices())
            {
                IValidationResult vResult = await authorisor.ValidatePin(request.CardNumber, request.PIN);

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
    }
}