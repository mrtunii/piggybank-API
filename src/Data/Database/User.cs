using System.Collections;
using System.Collections.Generic;

namespace Data.Database
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public int Point { get; set; }
        public decimal Amount { get; set; }

        public ICollection<Transaction> Transactions { get; } = new HashSet<Transaction>();
    }
}