
using BankingApp.Domain.DTOs;

namespace BankingApp.Domain.TransactionValidators
{
    public interface IValidationsManager
    {
        Task<ValidatorResult> ValidateAsync(TransactionPostRequestModel transaction);
    }

    public class ValidationsManager:IValidationsManager
    {
        private readonly ITransactionValidator _firstTransactionValidator;

        public ValidationsManager(List<ITransactionValidator> validators)
        {
            if (validators == null || validators.Count == 0)
            {
                return;
            }
            
            var currentValidator = _firstTransactionValidator = validators.First();

            foreach (var validator in validators.Skip(1))
            {
                currentValidator.Next = validator;
                currentValidator = validator;
            }

        }

        public async Task<ValidatorResult> ValidateAsync(TransactionPostRequestModel transaction)
        {
            if (_firstTransactionValidator == null)
            {
                return  new ValidatorResult();
            }

            return await _firstTransactionValidator.ValidateAsync(transaction);
        }
    }
}
