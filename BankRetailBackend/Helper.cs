using BankRetailBackend.Managers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BankRetailBackend
{
    public enum AvailableUseCases
    {
        GetAccount,
        Transfer,
        PutAccount,
        GetTransactions,
        GetTransactionsBetween,
        GetCustomers,
        GetAccounts,
        GetCustomer,
        GetCustomerBySSN,
        AddCustomer,
        UpdateCustomer,
        DeleteCustomer,
        AddAccount,
        DeleteAccount
    }

    public enum AuthenticationStatus
    {
        JWTFailed,
        RoleFailed,
        BadRequest,
        Unknown,
        Success
    }

    public static class Helper
    {
        private static string SecretKey = "Vfru3PuNevbsh5Cb2J4nh2sjM928P7pd";
        // 1 = executive
        // 2 = cashier
        private static Dictionary<AvailableUseCases, int[]> userCasesRoles = new Dictionary<AvailableUseCases, int[]>()
        {
            { AvailableUseCases.GetAccount, new int[]               { 1, 2 } },
            { AvailableUseCases.GetAccounts, new int[]              { 1, 2 } },
            { AvailableUseCases.GetCustomers, new int[]             { 1, 2 } },
            { AvailableUseCases.GetCustomer, new int[]              { 1, 2 } },
            { AvailableUseCases.GetCustomerBySSN, new int[]         { 1, 2 } },
            { AvailableUseCases.Transfer, new int[]                 { 2    } },
            { AvailableUseCases.PutAccount, new int[]               { 2    } },
            { AvailableUseCases.GetTransactions, new int[]          { 2    } },
            { AvailableUseCases.GetTransactionsBetween, new int[]   { 2    } },
            { AvailableUseCases.AddCustomer, new int[]              { 1    } },
            { AvailableUseCases.UpdateCustomer, new int[]           { 1    } },
            { AvailableUseCases.DeleteCustomer, new int[]           { 1    } },
            { AvailableUseCases.AddAccount, new int[]               { 1    } },
            { AvailableUseCases.DeleteAccount, new int[]            { 1    } },

        };

        public static AuthenticationStatus AuthenticationHelper(HttpRequest request, AvailableUseCases useCase)
        {
            string Authentication = request.Headers["Authentication"];
            if(string.IsNullOrEmpty(Authentication))
            {
                return AuthenticationStatus.BadRequest;
            }


            JWTService service = new JWTService(SecretKey);

            try
            {
                IEnumerable<Claim> claims = service.GetTokenClaims(Authentication);

                if(claims == null)
                {
                    return AuthenticationStatus.JWTFailed;
                }

                string roleValue = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value; 

                if (!service.isTokenValid(Authentication))
                {
                    return AuthenticationStatus.JWTFailed;
                }

                // TODO: Check if I have permission to acces this 
                if (userCasesRoles[useCase].Contains(Convert.ToInt32(roleValue)) == false)
                {
                    return AuthenticationStatus.RoleFailed;
                }

            }
            catch (Exception)
            {
                return AuthenticationStatus.Unknown;
            }

            return AuthenticationStatus.Success;
        }
        //public static bool AuthenticationHelper(HttpRequest request, AvailableUseCases useCase)
        //{
        //    string Authentication = request.Headers["Authentication"];
        //    JWTService service = new JWTService(SecretKey);

        //    IEnumerable<Claim> claims = service.GetTokenClaims(Authentication);
        //    string roleValue = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;


        //    try
        //    {
        //        if (!service.isTokenValid(Authentication))
        //        {
        //            return false;
        //        }

        //        // TODO: Check if I have permission to acces this 
        //        if (userCasesRoles[useCase].Contains(Convert.ToInt32(roleValue)) == false)
        //        {
        //            return false;
        //        }

        //    }
        //    catch(Exception)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}
