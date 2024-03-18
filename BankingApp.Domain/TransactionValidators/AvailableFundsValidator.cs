using BankingApp.Domain.DTOs;
using BankingApp.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Domain.TransactionValidators
{
    public class AvailableFundsValidator : ITransactionValidator
    {
        private readonly BankingAppDbContext _bankingAppDbContext;

        public AvailableFundsValidator(BankingAppDbContext bankingAppDbContext)
        {
            _bankingAppDbContext = bankingAppDbContext;
        }

        public ITransactionValidator Next {get; set;}

        public async Task<ValidatorResult> ValidateAsync(TransactionPostRequestModel transaction)
        {
            var payerAccount = await _bankingAppDbContext.Account.Where(w => w.Id == transaction.PayerAccount).FirstAsync();

            if (payerAccount.Balance < transaction.Amount)
            {
                return new ValidatorResult 
                { 
                    IsValid = false,
                    Error = Constants.TransactionError.InsuficientFunds,
                    ErrorMessage = "Insuficient Funds"
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
