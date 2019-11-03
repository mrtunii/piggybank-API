using System;
using System.Linq;
using System.Threading.Tasks;
using API.Filters;
using API.Models.ApiResponses;
using Core.Services.Interfaces;
using Data.Request.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ModelStateValidationFilter]
    [Route("api/transactions")]
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var authUserId =
                Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value);
            var transactions = await _transactionService.GetAllAsync(authUserId);

            return Ok(new ApiOkResponse(transactions));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionRequest request)
        {
            var authUserId =
                Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id").Value);
            request.UserId = authUserId;

            var transaction = await _transactionService.CreateAsync(request);

            return Ok(new ApiOkResponse(transaction));
        }
    }
}