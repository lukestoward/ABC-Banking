using System;
using System.Collections.Generic;
using System.Linq;

namespace ABC_Banking.Core.Validation
{
    public class ValidationResult : IValidationResult
    {
        //Overloaded Constructor
        public ValidationResult(): this(new List<string>(), new List<Exception>())
        {
        }

        public ValidationResult(List<string> errors, List<Exception> exceptions)
        {
            Errors = errors;
            Exceptions = exceptions;
        }


        //Lists
        public List<string> Errors { get; }

        public List<Exception> Exceptions { get; }
        

        // Methods
        public bool HasError()
        {
            return Errors.Any() || Exceptions.Any();
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }

        public void AddException(Exception ex)
        {
            Exceptions.Add(ex);
        }
    }
}