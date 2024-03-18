using BankingApp.Domain.DTOs;
using BankingApp.Models.DatabaseModels;

namespace BankingApp.Domain.TransactionDomain
{
    public interface IBankingService
    {
        Task<Account> GetClientAsync(string accountId);
        Task<List<Transaction>> GetTransactionsAsync(string accountId);
        Task<decimal> GetBalanceAsync(string accountId);
        Task<string> CreateTransactionAsync (TransactionPostRequestModel transaction);
        Task<string> GetTransactionStatusAsync(string transactionId); 
    }
}
