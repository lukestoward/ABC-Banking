using System.Collections.Generic;
using System.Linq;

namespace ABC_Banking.Services.Deposit.Validation
{
    public class ValidationResult : IValidationResult
    {
        //Overloaded 
        public ValidationResult(): this(new List<string>())
        {
        }

        public ValidationResult(List<string> errors)
        {
            Errors = errors;
        }

        public List<string> Errors { get; }

        bool IValidationResult.HasError()
        {
            return Errors.Any();
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}