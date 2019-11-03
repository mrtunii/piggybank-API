using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Data.Database
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string PasswordSalt { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public int Point { get; set; }
        public decimal Amount { get; set; }
        public int Level { get; set; }
        public Guid? AchievementId { get; set; }

        public ICollection<Transaction> Transactions { get; } = new HashSet<Transaction>();
        public virtual Achievement Achievement { get; set; }
    }
}