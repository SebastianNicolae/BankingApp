using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models.DatabaseModels
{
    public class Account
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Iban { get; set; }
    }
}
