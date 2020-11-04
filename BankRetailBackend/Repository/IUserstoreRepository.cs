using BankRetailBackend.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankRetailBackend.Repository
{
    public interface IUserstoreRepository
    {
        bool isValid(string LoginId, string Pw);

        Userstore getUser(string LoginId, string Pw);
    }
}
