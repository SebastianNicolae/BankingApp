using BankingApp.Common;
using BankingApp.Domain.Constants;
using BankingApp.Domain.DTOs;
using BankingApp.Domain.TransactionDomain;
using BankingApp.Domain.TransactionValidators;
using BankingApp.Extensions.ApiResponse;
using BankingApp.Models;
using BankingApp.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BankingApp.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/transactions")]
    public class TransactionController:ControllerBase
    {
        private readonly IBankingService _bankingService;
        private readonly IValidationsManager _validationsManager;
        private readonly IResponseFactory _responseFactory;
        private readonly BankingAppDbContext _bankingAppDbContext;

        public TransactionController(IBankingService bankingService, IValidationsManager validationsManager, BankingAppDbContext bankingAppDbContext, IResponseFactory responseFactory)
        {
            _bankingService = bankingService;
            _validationsManager = validationsManager;
            _bankingAppDbContext = bankingAppDbContext;
            _responseFactory = responseFactory;
        }

        [HttpPost]
      
        public async Task<ActionResult> CreateTransactionAsync(TransactionPostRequestModel transaction)
        {

            if (transaction==null)
                return BadRequest(_responseFactory.CreateErrorResponse(PredefinedErrors.General.BadRequest));

            var validatorResult = await _validationsManager.ValidateAsync(transaction);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.ErrorMessage);
            }
        
            var transactionId = await _bankingService.CreateTransactionAsync(transaction);

            return Ok(transactionId);
        }


        [HttpGet("{transactionId}")]
        public async Task<ActionResult> GetTransactionStatusAsync(string transactionId)
        {
            var status =  await _bankingService.GetTransactionStatusAsync(transactionId);
            return Ok(status);
        }
    }
}
