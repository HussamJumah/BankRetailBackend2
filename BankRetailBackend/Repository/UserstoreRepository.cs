using BankRetailBackend.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankRetailBackend.Repository
{
    public class UserstoreRepository : IUserstoreRepository
    {
        private readonly BankRetailBackendContext _dbcontext;
        public UserstoreRepository(BankRetailBackendContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public Userstore getUser(string LoginId, string Pw)
        {
            return (_dbcontext.Userstore.FirstOrDefault(user => user.LoginId == LoginId && user.Pw == Pw));
        }

        public bool isValid(string LoginId, string Pw)
        {
            return (getUser(LoginId, Pw) != null);
        }
    }
}
