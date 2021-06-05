using AfterMe.Core.Accounts.Entities;
using System.Collections.Generic;

namespace AfterMe.Core.Accounts.Infrastructures.Inmem
{
    /// <summary>
    /// The implementation of the IAccountRepository by using in memory as its storage
    /// </summary>
    public class AccountInmem : IAccountRepository
    {
        List<Account> inMemoryAccountsStorage;

        public AccountInmem()
        {
            this.inMemoryAccountsStorage = new List<Account>();
        }

        private int NewID()
        {
            return this.inMemoryAccountsStorage.Count + 1;
        }

        /// <summary>
        /// Adds a new account into memory
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Account Add(Account account)
        {
            account.Id = NewID();
            inMemoryAccountsStorage.Add(account);
            return account;
        }

        /// <summary>
        /// Deletes an existing account by its id
        /// </summary>
        /// <param name="accountId"></param>
        public void Delete(int accountId)
        {
            foreach (Account account in inMemoryAccountsStorage)
            {
                if (account.Id == accountId)
                {
                    inMemoryAccountsStorage.Remove(account);
                    break;
                }
            }
        }

        /// <summary>
        /// Returns an account if found by the specified email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Account GetByEmail(string email)
        {
            foreach (Account account in inMemoryAccountsStorage)
            {
                if (account.Email == email)
                {
                    return account;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns an account if match with the specified id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Account GetById(int accountId)
        {
            foreach (Account account in inMemoryAccountsStorage)
            {
                if (account.Id == accountId)
                {
                    inMemoryAccountsStorage.Remove(account);
                    break;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns list of accounts where the accounts contain name in the specified request
        /// </summary>
        /// <param name="listRequest"></param>
        /// <returns></returns>
        public List<Account> List(AccountListRequest listRequest)
        {
            List<Account> accounts = new List<Account>();
            foreach (Account account in inMemoryAccountsStorage)
            {
                if (account.Name.Contains(listRequest.Name))
                {
                    accounts.Add(account);
                }
            }
            return accounts;
        }

        /// <summary>
        /// Updates the existing account in the storage
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Account Update(Account account)
        {
            Delete(account.Id);
            return Add(account);
        }
    }
}
