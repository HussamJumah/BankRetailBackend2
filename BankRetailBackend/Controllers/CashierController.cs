using System.Threading.Tasks;
using BankRetailBackend.DBModels;
using BankRetailBackend.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Globalization;
using BankRetailBackend.Managers;

namespace BankRetailBackend.Controllers
{
    [Route("cashier/accounts")]
    [ApiController]
    public class CashierController : ControllerBase
    {
        private readonly ICashierRepository _CashierRepo;

        public CashierController(ICashierRepository repository)
        {
            _CashierRepo = repository;
        }

        // GET: cashier/accounts/{ID}
        [HttpGet("{ID}")]
        // this take in a single customer ID and get all account associated with that customer
        public async Task<ActionResult<IEnumerable<AccountStatus>>> GetAccountsForCustomer(int ID)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.GetAccount))
            {
                case AuthenticationStatus.JWTFailed:
                    return Unauthorized();
                case AuthenticationStatus.RoleFailed:
                    return StatusCode(403);
                case AuthenticationStatus.Unknown:
                    return StatusCode(500);
                case AuthenticationStatus.BadRequest:
                    return BadRequest();
            }
            return await _CashierRepo.GetAccounts(ID);
        }

        // GET: cashier/accounts/{ID}
        [HttpGet("{ID}/active")]
        // this take in a single customer ID and get all account associated with that customer
        public async Task<ActionResult<IEnumerable<AccountStatus>>> GetActiveAccountsForCustomer(int ID)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.GetAccount))
            {
                case AuthenticationStatus.JWTFailed:
                    return Unauthorized();
                case AuthenticationStatus.RoleFailed:
                    return StatusCode(403);
                case AuthenticationStatus.Unknown:
                    return StatusCode(500);
                case AuthenticationStatus.BadRequest:
                    return BadRequest();
            }

            return await _CashierRepo.GetActiveAccounts(ID);
        }

        // PUT: cashier/accounts/{ID}/{opCode}/{amount}
        [HttpPut("{ID}/{opCode}/{amount}")]
        public async Task<ActionResult<AccountStatus>> PutAccount(int ID, int opCode, AccountStatus account, double amount)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.PutAccount))
            {
                case AuthenticationStatus.JWTFailed:
                    return Unauthorized();
                case AuthenticationStatus.RoleFailed:
                    return StatusCode(403);
                case AuthenticationStatus.Unknown:
                    return StatusCode(500);
                case AuthenticationStatus.BadRequest:
                    return BadRequest();
            }

            if (ID != account.AccountId || account.Status.ToLower().Equals("inactive"))
            {
                return BadRequest();
            }
            return await _CashierRepo.PutAccount(opCode, account, amount);
        }

        // PUT: cashier/accounts/transfer/{senderID}/{receiverID}/{amount}
        [HttpPut("transfer/{senderID}/{receiverID}/{amount}")]
        public async Task<ActionResult<IEnumerable<AccountStatus>>> Transfer(int senderID, int receiverID, double amount)
        {

            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.Transfer))
            {
                case AuthenticationStatus.JWTFailed:
                    return Unauthorized();
                case AuthenticationStatus.RoleFailed:
                    return StatusCode(403);
                case AuthenticationStatus.Unknown:
                    return StatusCode(500);
                case AuthenticationStatus.BadRequest:
                    return BadRequest();
            }
            var updatedAccounts = await _CashierRepo.Transfer(senderID, receiverID, amount);
            if (updatedAccounts == null)
            {
                return BadRequest();
            }
            return updatedAccounts;

        }

        // GET: cashier/accounts/{accountID}/n={numTransactions}
        [HttpGet("{accountID}/n={numTransactions}")]
        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransactions(int accountID, int numTransactions)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.GetTransactions))
            {
                case AuthenticationStatus.JWTFailed:
                    return Unauthorized();
                case AuthenticationStatus.RoleFailed:
                    return StatusCode(403);
                case AuthenticationStatus.Unknown:
                    return StatusCode(500);
                case AuthenticationStatus.BadRequest:
                    return BadRequest();
            }
            return await _CashierRepo.GetTransactions(accountID, numTransactions);
        }

        // GET: cashier/accounts/{accountID}/yyyyMMdd/yyyyMMdd
        [HttpGet("{accountID}/{startDateString}/{endDateString}")]
        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransactionsBetween(int accountID, string startDateString, string endDateString)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.GetTransactionsBetween))
            {
                case AuthenticationStatus.JWTFailed:
                    return Unauthorized();
                case AuthenticationStatus.RoleFailed:
                    return StatusCode(403);
                case AuthenticationStatus.Unknown:
                    return StatusCode(500);
                case AuthenticationStatus.BadRequest:
                    return BadRequest();
            }

            DateTime startDate = DateTime.ParseExact(startDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(endDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
            return await _CashierRepo.GetTransactionsBetween(accountID, startDate, endDate);
        }

    }
}
