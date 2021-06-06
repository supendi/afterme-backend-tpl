using AfterMe.Core.Domains.Accounts.Entities;
using AfterMe.Core.InternalLib;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace AfterMe.Core.Domains.Accounts
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
    public class AccountService : UserManager<Account>
    {
        public AccountService(IUserStore<Account> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Account> passwordHasher, IEnumerable<IUserValidator<Account>> userValidators, IEnumerable<IPasswordValidator<Account>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Account>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
