using System;
using System.Collections.Generic;

namespace BankRetailBackend.DBModels
{
    public partial class Transactions
    {
        public int TransactionId { get; set; }
        public int? AccountId { get; set; }
        public string TransactionType { get; set; }
        public double? TransactionAmount { get; set; }
        public double? Balance { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual AccountStatus Account { get; set; }
    }
}
