using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankRetailBackend.SessionModels
{
    public interface IAuthContainerModel
    {
        string SecretKey { get; set; }
        string SecretAlgorithm { get; set; }
        int ExpireMinutes { get; set; }

        Claim [] Claims { get; set; }
    }
}
