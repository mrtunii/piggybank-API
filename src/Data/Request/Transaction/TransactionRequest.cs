using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Request.Transaction
{
    public class TransactionRequest
    {
        public string AccountNumber
        {
            get { return Guid.NewGuid().ToString(); }
        }
        [Required(ErrorMessage = "ტრანზაქციის თანხა ცარიელია")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "მერჩანტი ცარიელია")]
        public string MerchantName { get; set; }

        public DateTime TransactionDate
        {
            get { return DateTime.Now; }
        }
        
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}