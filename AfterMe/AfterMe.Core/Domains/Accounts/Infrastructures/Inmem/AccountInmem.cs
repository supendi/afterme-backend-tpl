using AfterMe.Core.Domains.Accounts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AfterMe.Core.Domains.Accounts.Infrastructures.Inmem
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

        private string NewID()
        {
            return (this.inMemoryAccountsStorage.Count + 1).ToString();
        }

        /// <summary>
        /// Adds a new account into memory
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Task<Account> Add(Account account)
        {
            account.Id = NewID();
            inMemoryAccountsStorage.Add(account);
            return Task.FromResult(account);
        }

        /// <summary>
        /// Deletes an existing account by its id
        /// </summary>
        /// <param name="accountId"></param>
        public void Delete(string accountId)
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
        public Task<Account> GetByEmail(string email)
        {
            foreach (Account account in inMemoryAccountsStorage)
            {
                if (account.Email == email)
                {
                    return Task.FromResult(account);
                }
            }
            return null;
        }

        /// <summary>
        /// Returns an account if match with the specified id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Task<Account> FindById(params object[] keys)
        {
            foreach (Account account in inMemoryAccountsStorage)
            {
                if (account.Id == keys[0].ToString())
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
        public Task<List<Account>> List(AccountListRequest listRequest)
        {
            List<Account> accounts = new List<Account>();
            foreach (Account account in inMemoryAccountsStorage)
            {
                if (account.Name.Contains(listRequest.Name))
                {
                    accounts.Add(account);
                }
            }
            return Task.FromResult(accounts);
        }

        /// <summary>
        /// Updates the existing account in the storage
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Task<Account> Update(Account account)
        {
            Delete(account.Id);
            return Add(account);
        }
    }
}
