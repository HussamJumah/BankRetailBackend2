using BankRetailBackend.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankRetailBackend.Repository
{
    public class ExecutiveRepository : IExecutiveRepository
    {
        private readonly BankRetailBackendContext _context;

        public ExecutiveRepository(BankRetailBackendContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<CustomerStatus> AddCustomerInfo(CustomerStatus customer)
        {
            customer.Status = "active";
            _context.CustomerStatus.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<CustomerStatus> UpdateCustomerInfo(int customerId, CustomerStatus customer)
        {
            customer.LastUpdated = DateTime.UtcNow;
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.CustomerStatus.Find(customerId) == null)
                {
                    return customer;
                }
                throw;
            }
            return customer;
        }

        public async Task<CustomerStatus> DeleteCustomerInfo(int customerId)
        {
            var customer = await _context.CustomerStatus.FindAsync(customerId);
            if (customer == null)
            {
                return customer;
            }

            customer.Status = "inactive";
            List<AccountStatus> accounts = await GetAccountsById(customerId);
            foreach (AccountStatus account in accounts)
            {
                if (account.Status.ToLower().Equals("active"))
                {
                    account.Status = "inactive";
                }
            }
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<CustomerStatus> GetCustomerInfo(int customerId)
        {
            return await _context.CustomerStatus.FindAsync(customerId);
        }

        public async Task<CustomerStatus> GetCustomerInfoBySSN(int SSN)
        {
            return await _context.CustomerStatus.FirstOrDefaultAsync(cust => cust.Ssn.Equals(SSN));
        }

        public async Task<AccountStatus> AddAccountInfo(AccountStatus account)
        {
            account.Status = "active";
            _context.AccountStatus.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<AccountStatus> DeleteAccountInfo(int accountId)
        {
            var account = await _context.AccountStatus.FindAsync(accountId);
            if (account == null)
            {
                return account;
            }

            if (account.Status.ToLower().Equals("active"))
            {
                account.Status = "inactive";
            }
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<AccountStatus> GetAccountInfo(int accountId)
        {
            return await _context.AccountStatus.FindAsync(accountId);
        }

        public async Task<List<CustomerStatus>> GetAllCustomers()
        {
            return await _context.CustomerStatus.ToListAsync();
        }

        public async Task<List<AccountStatus>> GetAllAccounts()
        {
            return await _context.AccountStatus.ToListAsync();
        }

        public async Task<List<AccountStatus>> GetAccountsById(int customerId)
        {
            return await _context.AccountStatus.Where(account => account.CustomerId == customerId).ToListAsync();
        }
    }
}