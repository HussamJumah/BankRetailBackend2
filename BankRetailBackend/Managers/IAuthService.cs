using BankRetailBackend.SessionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankRetailBackend.Managers
{
    public interface IAuthService
    {
        string secretKey { get; set; }

        bool isTokenValid(string token);

        string generateToken(IAuthContainerModel model);

        IEnumerable<Claim> GetTokenClaims(string token);

    }
}

