using System;

namespace Data.Database
{
    public class Transaction : BaseEntity
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string MerchantName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public bool HasProcessed { get; set; }
        public decimal ProcessedAmount { get; set; }
        public int ProcessedPoint { get; set; } 
        public Guid UserId { get; set; }
        
        public virtual User User { get; set; }
    }
}