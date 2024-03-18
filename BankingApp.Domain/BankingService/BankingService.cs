using BankingApp.Domain.DTOs;
using BankingApp.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Domain.TransactionDomain
{
    public class BankingService:IBankingService
    {
        private readonly BankingAppDbContext _bankingAppDbContext;

        public BankingService(BankingAppDbContext bankingAppDbContext)
        {
            _bankingAppDbContext = bankingAppDbContext;
        }

        public async Task<Account> GetClientAsync(string accountId)
        {
           return await _bankingAppDbContext.Account.FirstAsync(w => w.Id == accountId);
        }
        public async Task<List<Transaction>> GetTransactionsAsync(string accountId)
        {
          
            return await  _bankingAppDbContext.Transaction
                .Where(w => w.PayerAccount == accountId || w.ReceiverAccount == accountId).ToListAsync();

        }

        public async Task<decimal> GetBalanceAsync(string accountId)
        {
           
            var account = await _bankingAppDbContext.Account.FirstAsync(w=>w.Id == accountId);

            return account.Balance;
        }

        public async Task<string> CreateTransactionAsync(TransactionPostRequestModel transaction)
        {
            var payerAccount = await _bankingAppDbContext.Account
                .Where(w=>w.Id == transaction.PayerAccount).FirstAsync();

            var receiverAccount = await _bankingAppDbContext.Account
                .Where(w=>w.Id == transaction.ReceiverAccount).FirstAsync();

            var transactionEntity = new Transaction {

                TransactionId = Guid.NewGuid().ToString(),
                PayerAccount = transaction.PayerAccount,
                ReceiverAccount = transaction.ReceiverAccount,
                Amount = transaction.Amount,
                Status = "In Process",
                Currency = transaction.Currency,
                DateCreated = DateTime.UtcNow
            };

           _bankingAppDbContext.Transaction.Add(transactionEntity);

            payerAccount.Balance -= transaction.Amount;

            receiverAccount.Balance += transaction.Amount;

            await _bankingAppDbContext.SaveChangesAsync();
            return transactionEntity.TransactionId;
        }

        public async Task<string> GetTransactionStatusAsync(string transactionId)
        {
            return await _bankingAppDbContext.Transaction
            .Where(ts => ts.TransactionId == transactionId)
            .Select(ts => ts.Status)
            .FirstAsync();
        }

    }
}