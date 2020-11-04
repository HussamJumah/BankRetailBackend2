using BankRetailBackend.DBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankRetailBackend.Repository
{
    public class CashierRepository : ICashierRepository
    {
        private readonly BankRetailBackendContext _BRBContext;

        public CashierRepository(BankRetailBackendContext context)
        {
            _BRBContext = context;
            _BRBContext.Database.EnsureCreated();
        }

        // find the account using the primary key AccountId
        public async Task<ActionResult<IEnumerable<AccountStatus>>> GetAccounts(int ID)
        {
            return await _BRBContext.AccountStatus.Where(acc => acc.CustomerId.Equals(ID)).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<AccountStatus>>> GetActiveAccounts(int ID)
        {
            return await _BRBContext.AccountStatus.Where(acc => acc.CustomerId.Equals(ID) && acc.Status == "active").ToListAsync();
        }

        // updating the account, opCode = 1 for deposit, 2 for withdraw, 3 for transfer-deposit, 4 for transfer-withdraw
        public async Task<ActionResult<AccountStatus>> PutAccount(int opCode, AccountStatus account, double amount)
        {
            string transaction = "";
            // switch statement to set the string for transaction type
            switch (opCode)
            {
                case 1:
                    transaction = "Deposit";
                    break;
                case 2:
                    transaction = "Withdraw";
                    break;
                case 3:
                    transaction = "Transfer-Deposit";
                    break;
                case 4:
                    transaction = "Transfer-Withdraw";
                    break;
            }
            // switch statement to perform the operation
            switch (opCode)
            {
                case 1: //deposit
                case 3: //transfer-deposit
                    Console.WriteLine(account.Balance.HasValue);
                    account.Balance += amount;
                    account.LastUpdated = DateTime.Now;
                    _BRBContext.Update(account);
                    await _BRBContext.SaveChangesAsync();
                    // creating a new deposit transation and add it to our database
                    Transactions depositTransaction = new Transactions()
                    {
                        AccountId = account.AccountId,
                        Balance = account.Balance,
                        TransactionAmount = amount,
                        TransactionType = transaction,
                        TransactionDate = DateTime.Now
                    };
                    _BRBContext.Transactions.Add(depositTransaction);
                    //_BRBContext.Add(depositTransaction);
                    await _BRBContext.SaveChangesAsync();
                    break;
                case 2: //withdraw
                case 4: //transfer-withdraw
                    // only process the withdraw if the account has enough balance
                    if (amount < account.Balance)
                    {
                        account.Balance -= amount;
                        account.LastUpdated = DateTime.Now;
                        _BRBContext.Update(account);
                        await _BRBContext.SaveChangesAsync();
                        //creating a new withdraw transaction and add it to our database
                        Transactions withdrawTransaction = new Transactions()
                        {
                            AccountId = account.AccountId,
                            Balance = account.Balance,
                            TransactionAmount = amount,
                            TransactionType = transaction,
                            TransactionDate = DateTime.Now
                        };
                        _BRBContext.Add(withdrawTransaction);
                        await _BRBContext.SaveChangesAsync();
                    }
                    break;
            }
            return account;
        }

        public async Task<ActionResult<IEnumerable<AccountStatus>>> Transfer(int senderID, int receiverID, double amount)
        {
            AccountStatus sender = await _BRBContext.AccountStatus.FindAsync(senderID);
            AccountStatus receiver = await _BRBContext.AccountStatus.FindAsync(receiverID);
            //only perform the transfer if the sender has enough balance, and both accounts exists and are active
            if (sender != null 
                && sender.Balance > amount 
                && receiver != null 
                && sender.Status.ToLower().Equals("active")
                && receiver.Status.ToLower().Equals("active"))
            {
                await PutAccount(4, sender, amount);
                await PutAccount(3, receiver, amount);
            }
            else
            {
                return null;
            }
            return new List<AccountStatus> { sender, receiver };
        }

        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransactions(int accountID, int numTransactions)
        {
            return await _BRBContext.Transactions
                .Where(trans => trans.AccountId.Equals(accountID))
                .OrderByDescending(trans => trans.TransactionDate)
                .Take(numTransactions).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransactionsBetween(int accountID, DateTime startDate, DateTime endDate)
        {
            return await _BRBContext.Transactions
                .Where(trans => trans.AccountId.Equals(accountID))
                .Where(trans => trans.TransactionDate >= startDate)
                .Where(trans => trans.TransactionDate <= endDate).ToListAsync();
        }
    }
}
