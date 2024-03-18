using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Extensions.ApiResponse
{
    public class PredefinedError
    {
        public string Code { get; }
        public string Title { get; }
        public string Detail { get; }

        public PredefinedError(string code, string title, string detail = "")
        {
            Title = title;
            Detail = detail;
            Code = code;
        }
    }
}
