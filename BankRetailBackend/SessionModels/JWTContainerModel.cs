    using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankRetailBackend.SessionModels
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public string SecretKey { get; set; } = "Vfru3PuNevbsh5Cb2J4nh2sjM928P7pd";
        public string SecretAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 10080; //7 DAYS long
        public Claim[] Claims { get; set; }

        public static JWTContainerModel GetJWTContainerModel(string LoginId, string pw, int? RoleId)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, LoginId),
                    new Claim(ClaimTypes.Authentication, pw),
                    new Claim(ClaimTypes.Role, Convert.ToString(RoleId))
                }
            };
        }
    }

}
