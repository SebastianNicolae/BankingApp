using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Extensions.ApiResponse
{
    public class Error
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string SupportId { get; set; } = Guid.NewGuid().ToString();
    }
}
