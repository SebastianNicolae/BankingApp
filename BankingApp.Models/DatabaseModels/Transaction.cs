using System.Transactions;

namespace BankingApp.Models.DatabaseModels
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string PayerAccount { get; set; } // can be an int cause it could be a different table and have a FK
        public string ReceiverAccount { get; set; } // can be an int cause it could be a different table and have a FK
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }

        public DateTime DateCreated { get; set; }
    }
}