using BankingApp.Models.DatabaseModels;

namespace BankingApp.Domain.DTOs
{
    public class TransactionPostRequestModel
    {
        public string PayerAccount { get; set; } // can be an int cause it could be a different table and have a FK
        public string ReceiverAccount { get; set; } // can be an int cause it could be a different table and have a FK
        public string Iban { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
