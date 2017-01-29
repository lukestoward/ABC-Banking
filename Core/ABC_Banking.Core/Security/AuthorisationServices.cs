using System;
using System.Threading.Tasks;
using ABC_Banking.Core.DataAccess;
using ABC_Banking.Core.Models.BankCards;
using ABC_Banking.Core.Validation;

namespace ABC_Banking.Core.Security
{
    public class AuthorisationServices : IDisposable
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public async Task<ValidationResult> ValidatePin(string cardNumber, int pin)
        {
            ValidationResult result = new ValidationResult();

            BankCard card = await _unitOfWork.BankCardRepository.GetFirst(x => x.CardNumber == cardNumber);

            if (card == null)
            {
                result.AddError("PIN validation failed");
                return result;
            }

            if (card.Pin != pin)
            {
                result.AddError("PIN validation failed");
            }

            return result;
        }


        #region Dispose

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
