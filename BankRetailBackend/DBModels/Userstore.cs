using System;
using System.Collections.Generic;

namespace BankRetailBackend.DBModels
{
    public partial class Userstore
    {
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string Pw { get; set; }
        public int? RoleId { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
