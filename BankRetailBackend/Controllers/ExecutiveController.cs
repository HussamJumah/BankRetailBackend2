using BankRetailBackend.DBModels;
using BankRetailBackend.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankRetailBackend.Controllers
{
    [Route("executive")]
    [ApiController]
    public class ExecutiveController : ControllerBase
    {
        private readonly IExecutiveRepository _repository;

        public ExecutiveController(IExecutiveRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.GetCustomers))
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

            return Ok(await _repository.GetAllCustomers());
        }

        [HttpGet("accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.GetAccounts))
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

            return Ok(await _repository.GetAllAccounts());
        }

        [HttpGet("customers/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.GetCustomer))
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

            var customer = await _repository.GetCustomerInfo(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("customers/ssn/{SSN}")]
        public async Task<IActionResult> GetCustomerBySSN(int SSN)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.GetCustomer))
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

            var customer = await _repository.GetCustomerInfoBySSN(SSN);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("accounts/{id}")]
        public async Task<IActionResult> GetAccount(int id)
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

            var account = await _repository.GetAccountInfo(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost("customers")]
        public async Task<ActionResult<CustomerStatus>> AddCustomer([FromBody] CustomerStatus customer)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.AddCustomer))
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

            await _repository.AddCustomerInfo(customer);
            return CreatedAtAction(
                "GetCustomer",
                new { id = customer.CustomerId },
                customer
            );
        }

        [HttpPut("customers/{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerStatus customer)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.UpdateCustomer))
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

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            await _repository.UpdateCustomerInfo(id, customer);
            return NoContent();
        }

        [HttpDelete("customers/{id}")]
        public async Task<ActionResult<CustomerStatus>> DeleteCustomer(int id)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.DeleteCustomer))
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

            var customer = await _repository.DeleteCustomerInfo(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost("accounts")]
        public async Task<ActionResult<AccountStatus>> AddAccount([FromBody] AccountStatus account)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.AddAccount))
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

            await _repository.AddAccountInfo(account);
            return CreatedAtAction(
                "GetAccount",
                new { id = account.AccountId },
                account
            );
        }

        [HttpDelete("accounts/{id}")]
        public async Task<ActionResult<AccountStatus>> DeleteAccount(int id)
        {
            switch (Helper.AuthenticationHelper(Request, AvailableUseCases.DeleteAccount))
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

            var account = await _repository.DeleteAccountInfo(id);
            if (account == null)
            {
                return NotFound();
            }
            return account;
        }
    }
}