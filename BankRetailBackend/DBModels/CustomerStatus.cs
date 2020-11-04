using System;
using System.Collections.Generic;

namespace BankRetailBackend.DBModels
{
    public partial class CustomerStatus
    {
        public CustomerStatus()
        {
            AccountStatus = new HashSet<AccountStatus>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? Ssn { get; set; }
        public int? CustomerAge { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Message { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Status { get; set; }

        public virtual ICollection<AccountStatus> AccountStatus { get; set; }
    }
}
