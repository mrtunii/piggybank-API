using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Database;
using Data.Request.Transaction;

namespace Core.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllAsync(Guid userId);
        Task<Transaction> CreateAsync(TransactionRequest request);
    }
}