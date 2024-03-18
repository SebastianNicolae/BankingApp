using BankingApp.Common;
using BankingApp.Domain.Constants;
using BankingApp.Domain.TransactionDomain;
using BankingApp.Domain.TransactionValidators;
using BankingApp.Extensions.ApiResponse;
using BankingApp.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BankingApp.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IBankingService _bankingService;
        private readonly IResponseFactory _responseFactory;

        public AccountController(IBankingService bankingService, IResponseFactory responseFactory)
        {
            _bankingService = bankingService;
            _responseFactory = responseFactory;
        }



        [HttpGet("{accountId}/transactions")]
        [SwaggerResponse(200, "Ok", typeof(IResponse<List<Transaction>>))]
        [SwaggerResponse(400, "Invalid request", typeof(Response<GenericErrorResponse>))]
        [SwaggerResponse(404, "Account not found", typeof(Response<GenericErrorResponse>))]
        [SwaggerResponse(500, "Internal server error", typeof(Response<GenericErrorResponse>))]
        public async Task<ActionResult> GetTransacationsAsync(string accountId)
        {
            var client = await _bankingService.GetClientAsync(accountId);

            if (client == null)
                return NotFound(_responseFactory.CreateErrorResponse(PredefinedErrors.Specific.ClientDoesntExist));

            var transactions = await _bankingService.GetTransactionsAsync(accountId);

            

            return Ok(transactions);
        }

        [HttpGet("{accountId}/balance")]
        [SwaggerResponse(200, "Ok", typeof(IResponse<Account>))]
        [SwaggerResponse(400, "Invalid request", typeof(Response<GenericErrorResponse>))]
        [SwaggerResponse(404, "Account not found", typeof(Response<GenericErrorResponse>))]
        [SwaggerResponse(500, "Internal server error", typeof(Response<GenericErrorResponse>))]
        public async Task<ActionResult> GetBalanceAsync(string accountId)
        {
            var client = await _bankingService.GetClientAsync(accountId);

            if (client == null)
                return NotFound(_responseFactory.CreateErrorResponse(PredefinedErrors.Specific.ClientDoesntExist));

            var balance = await _bankingService.GetBalanceAsync(accountId);
            return Ok(balance);

        }

    }
}
