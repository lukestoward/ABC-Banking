using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
