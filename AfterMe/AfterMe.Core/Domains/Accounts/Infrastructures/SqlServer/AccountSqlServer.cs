using AfterMe.Core.Domains.Accounts.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AfterMe.Core.Domains.Accounts.Infrastructures.SqlServer
{
    /// <summary>
    /// The implementation of the IAccountRepository by using in SQL Server as its persistent storage
    /// </summary>
    public class AccountSqlServer : UserStore<Account>
    {
        AccountDbContext dbContext;
        public AccountSqlServer(AccountDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public Account Add(Account account)
        {
            var entry = dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
            return entry.Entity;
        }

        public void Delete(string accountId)
        {
            Account existingAccount = dbContext.Accounts.Find(accountId);
            if (existingAccount is not null)
            {
                dbContext.Accounts.Remove(existingAccount);
                dbContext.SaveChanges();
            }
        }

        public Account GetByEmail(string email)
        {
            return dbContext.Accounts.FirstOrDefault(x => x.Email == email);
        }

        public Account GetById(string accountId)
        {
            return dbContext.Accounts.Find(accountId);
        }

        public List<Account> List(AccountListRequest listRequest)
        {
            return dbContext.Accounts.Where(x => x.Name.Contains(listRequest.Name)).ToList();
        }

        public Account Update(Account account)
        {
            var existingAccount = GetById(account.Id);
            existingAccount.Name = account.Name;
            existingAccount.Email = account.Email;
            //existingAccount.Password = account.Password;
            existingAccount.UpdatedAt = DateTime.Now;

            dbContext.SaveChanges();

            return existingAccount;
        }
    }
}
