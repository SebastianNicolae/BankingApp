using BankingApp.Domain.DTOs;
using BankingApp.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Domain.TransactionValidators
{
    public class IBANValidator : ITransactionValidator
    {
        private readonly BankingAppDbContext _bankingAppDbContext;

        public IBANValidator(BankingAppDbContext bankingAppDbContext)
        {
            _bankingAppDbContext = bankingAppDbContext;
        }

        public ITransactionValidator Next { get; set; }

        public async Task<ValidatorResult> ValidateAsync(TransactionPostRequestModel transaction)
        {
            var payerAccount = await _bankingAppDbContext.Account.Where(w => w.Iban == transaction.Iban).FirstAsync();

            var iban = payerAccount.Iban.Replace(" ", "").ToUpper();

            if (!iban.StartsWith("RO") || iban.Length != 24)
            {
                return new ValidatorResult
                {
                    IsValid = false,
                    Error = Constants.TransactionError.IncorrectIBAN,
                    ErrorMessage = "IBAN Format is not correct"

                };
            }

            if (Next is null)
            {
                return new ValidatorResult();
            }

            else
            {
                return await Next.ValidateAsync(transaction);
            }

        }
    }


}
