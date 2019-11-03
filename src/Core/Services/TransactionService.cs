using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Data.Database;
using Data.Request.Transaction;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetAllAsync(Guid userId)
        {
            return await _context.Transactions.Where(c => c.UserId == userId && !c.DateDeleted.HasValue)
                .OrderByDescending(c => c.TransactionDate)
                .ToListAsync();
        }

        public async Task<Transaction> CreateAsync(TransactionRequest request)
        {
            var newTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountNumber = request.AccountNumber,
                Amount = request.Amount,
                Description = request.Description,
                MerchantName = request.MerchantName,
                TransactionDate = request.TransactionDate,
                UserId = request.UserId
            };

            await _context.Transactions.AddAsync(newTransaction);

            await ProcessTransaction(newTransaction);
            
            await _context.SaveChangesAsync();
            return newTransaction;
        }

        private async Task ProcessTransaction(Transaction transaction)
        {
            if (transaction == null) throw new Exception("ტრანზაქცია არ არსებობს");

            var user = await _context.Users.FirstOrDefaultAsync(c =>
                c.Id == transaction.UserId && !c.DateDeleted.HasValue);
            if (user == null) throw new Exception("მომხმარებელი არ არსებობს");

            var maxRoundedAmount = Math.Ceiling(transaction.Amount);
            if (maxRoundedAmount == transaction.Amount) return;
            var depositableAmount = Math.Round(maxRoundedAmount - transaction.Amount, 2);
            
            transaction.ProcessedAmount = depositableAmount;
            transaction.ProcessedPoint = (int) (depositableAmount * 100);
            transaction.HasProcessed = true;
            
            user.Amount += depositableAmount;
            user.Point += transaction.ProcessedPoint;
        }
    }
}