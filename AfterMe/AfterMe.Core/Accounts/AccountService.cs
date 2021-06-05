using AfterMe.Core.Accounts.Entities;
using AfterMe.Core.InternalLib;
using System;
using System.Collections.Generic;

namespace AfterMe.Core.Accounts
{
    /// <summary>
    /// Represents the contracts of functionalities for working with account storage
    /// </summary>
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetByEmail(string email);
        List<Account> List(AccountListRequest listRequest);
    }

    /// <summary>
    /// Represents the abstract functionalities of a password hasher
    /// </summary>
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }


    /// <summary>
    /// Providing the Account business services like registers a new account, gets the account details, lists accounts, updates and deletes.
    /// </summary>
    public class AccountService
    {
        IAccountRepository accountRepository;
        IPasswordHasher passwordHasher;

        public AccountService(IAccountRepository accountRepository, IPasswordHasher passwordHasher)
        {
            this.accountRepository = accountRepository;
            this.passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Registers a new account
        /// </summary>
        /// <param name="registrant"></param>
        /// <returns></returns>
        public Account Register(Registrant registrant)
        {
            if (registrant is null)
            {
                throw new ArgumentNullException(nameof(registrant));
            }

            bool emailIsRegistered = this.accountRepository.GetByEmail(registrant.Email) != null;
            if (emailIsRegistered)
            {
                throw new ApplicationException(string.Format("The email '{0}' is already registerd. Try a new one", registrant.Email));
            }

            //We can use Automapper here
            Account newAccount = new Account()
            {
                Name = registrant.Name,
                Email = registrant.Email,
                Password = passwordHasher.Hash(registrant.Password),
                CreatedAt = DateTime.Now,
            };

            Account registeredAccount = accountRepository.Add(newAccount);
            return registeredAccount;
        }

        /// <summary>
        /// Updates an existing account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Account Update(Account account)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            bool accountIsExist = this.accountRepository.GetById(account.Id) != null;
            if (!accountIsExist)
            {
                throw new ApplicationException(string.Format("The account id '{0}' is not found.", account.Id));
            }

            Account updatedAccount = accountRepository.Update(account);
            return updatedAccount;
        }

        /// <summary>
        /// Gets an account by its id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Account Get(int accountId)
        {
            return accountRepository.GetById(accountId);
        }

        /// <summary>
        /// Returns a list of accounts by the specified list filters
        /// </summary>
        /// <param name="listRequest"></param>
        /// <returns></returns>
        public List<Account> List(AccountListRequest listRequest)
        {
            return accountRepository.List(listRequest);
        }

        /// <summary>
        /// Deletes an existing account by account id
        /// </summary>
        /// <param name="accountId"></param>
        public void Delete(int accountId)
        {
            accountRepository.Delete(accountId);
        }
    }
}
