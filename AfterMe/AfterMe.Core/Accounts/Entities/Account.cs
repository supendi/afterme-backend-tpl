using Microsoft.AspNetCore.Identity;
using System;

namespace AfterMe.Core.Accounts.Entities
{
    /// <summary>
    /// Represents the account entities
    /// </summary>
    public class Account : IdentityUser
    {
        public string Name { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }

    /// <summary>
    /// Represents the request model for registering a new account 
    /// </summary>
    public class Registrant
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Represents the request model for getting a list of accounts
    /// </summary>
    public class AccountListRequest
    {
        public string Name { get; set; }
    }
}
