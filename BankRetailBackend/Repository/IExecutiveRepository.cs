using BankRetailBackend.DBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankRetailBackend.Repository
{
    public interface IExecutiveRepository
    {
        Task<CustomerStatus> AddCustomerInfo(CustomerStatus customer);
        Task<CustomerStatus> UpdateCustomerInfo(int customerId, CustomerStatus customer);
        Task<CustomerStatus> DeleteCustomerInfo(int customerId);
        Task<CustomerStatus> GetCustomerInfo(int customerId);
        Task<CustomerStatus> GetCustomerInfoBySSN(int SSN);
        Task<AccountStatus> AddAccountInfo(AccountStatus account);
        Task<AccountStatus> DeleteAccountInfo(int accountId);
        Task<AccountStatus> GetAccountInfo(int accountId);
        Task<List<CustomerStatus>> GetAllCustomers();
        Task<List<AccountStatus>> GetAllAccounts();
    }
}