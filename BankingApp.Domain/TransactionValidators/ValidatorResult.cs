using BankingApp.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Domain.TransactionValidators
{
    public class ValidatorResult
    {
        public bool IsValid { get; set; }
        public TransactionError Error { get; set; }
        public string ErrorMessage { get; set; }
        public ValidatorResult(bool isValid = true, TransactionError error = TransactionError.Valid,string errorMessage = null)
        {
            IsValid = isValid;
            Error = error;
            ErrorMessage = errorMessage;
        }
    }
}
