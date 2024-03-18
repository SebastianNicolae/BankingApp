using BankingApp.Domain.DTOs;
using BankingApp.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Domain.TransactionValidators
{
    public interface ITransactionValidator
    {
        ITransactionValidator Next { get; set; }

        Task<ValidatorResult> ValidateAsync(TransactionPostRequestModel transaction);

    }
}
