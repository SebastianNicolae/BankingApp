using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Domain.Constants
{
    public enum TransactionError
    {
        Valid,
        InsuficientFunds,
        IncorrectIBAN
    }

}
