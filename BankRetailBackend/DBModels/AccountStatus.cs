using System;
using System.Collections.Generic;

namespace BankRetailBackend.DBModels
{
    public partial class AccountStatus
    {
        public AccountStatus()
        {
            Transactions = new HashSet<Transactions>();
        }

        public int AccountId { get; set; }
        public int? CustomerId { get; set; }
        public string AccountType { get; set; }
        public double? Balance { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual CustomerStatus Customer { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
