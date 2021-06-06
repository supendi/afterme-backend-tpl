using AfterMe.Core.Accounts;
using AfterMe.Core.Accounts.Entities;
using AfterMe.Core.Accounts.Infrastructures.SqlServer;
using AfterMe.Core.Accounts.Libs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text.Json;

namespace AfterMe.ConsoleApp
{
    /// <summary>
    /// This is an example program of how to use the AfterMe.Core Service APIs
    /// </summary>
    class Program
    {
        //static AccountDbContext dbContext = new AccountDbContext();
        //static IPasswordHasher passwordHasher = new PasswordHasher();
        //static IUserStore<Account> accountRepository = new AccountSqlServer(dbContext);
        //static AccountService accountService = new AccountService(accountRepository,PasswordHasherPasswordHasher );
        ////static AuthService authService= new AuthService(accountRepository, passwordHasher);
        static void Main(string[] args) { }
        //static void Main(string[] args)
        //{
        //    var registeredAccount = RegisterAccount();
        //    GetAccount(registeredAccount.Id);//check the registered account above if its already in the database
        //    UpdateAccount(registeredAccount.Id);//test the update
        //    GetAccount(registeredAccount.Id);//check if the account in database is updated;
        //    DeleteAccount(registeredAccount.Id);//test delete
        //    GetAccount(registeredAccount.Id);//check if the account in database is deleted;
        //    Console.ReadKey();
        //}

        //static Account RegisterAccount()
        //{
        //    Registrant registrant = new Registrant()
        //    {
        //        Name = "Irpan",
        //        Email = "irpan@geekseat.com.au",
        //        Password = "YouWontNoticeThisHardPassword"
        //    };

        //    Console.WriteLine(string.Format("Registering new account : {0}", JsonSerializer.Serialize(registrant)));

        //    //register
        //    var registeredAccount = accountService.Register(registrant);
        //    Console.WriteLine(string.Format("A new account has been registered with ID : {0}", registeredAccount.Id));
        //    return registeredAccount;
        //}

        //static void GetAccount(string accountId)
        //{
        //    var accountFromDatabase = accountService.Get(accountId);
        //    Console.WriteLine(string.Format("Account record in database : {0}", JsonSerializer.Serialize(accountFromDatabase)));
        //}

        //static void UpdateAccount(string accountId)
        //{
        //    var accountFromDatabase = accountService.Get(accountId);
        //    accountFromDatabase.Name = "Your name has been hacked";
        //    accountService.Update(accountFromDatabase);
        //    Console.WriteLine(string.Format("Updating account: {0}", JsonSerializer.Serialize(accountFromDatabase)));
        //}

        //static void DeleteAccount(string accountId)
        //{
        //    accountService.Delete(accountId);
        //    Console.WriteLine(string.Format("Deleting account with id: {0}", accountId));
        //}

        //static void Login(LoginRequest loginRequest)
        //{
        //}
    }
}
