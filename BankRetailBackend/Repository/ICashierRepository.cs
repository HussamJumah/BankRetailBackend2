using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankRetailBackend.DBModels;
using Microsoft.AspNetCore.Mvc;

namespace BankRetailBackend.Repository
{
    public interface ICashierRepository
    {
        //Cashier/teller: Get account, Update Account (deposit and withdraw), 
        //Transfer (from 1 account to another), pull account statement
        Task<ActionResult<IEnumerable<AccountStatus>>> GetAccounts(int ID);
        Task<ActionResult<IEnumerable<AccountStatus>>> GetActiveAccounts(int ID);
        Task<ActionResult<AccountStatus>> PutAccount(int opCode, AccountStatus account, double amount);
        Task<ActionResult<IEnumerable<AccountStatus>>> Transfer(int senderID, int receiverID, double amount);
        Task<ActionResult<IEnumerable<Transactions>>> GetTransactions(int accountID, int numTransactions);
        Task<ActionResult<IEnumerable<Transactions>>> GetTransactionsBetween(int accountID, DateTime startDate, DateTime endDate);
    }
}
