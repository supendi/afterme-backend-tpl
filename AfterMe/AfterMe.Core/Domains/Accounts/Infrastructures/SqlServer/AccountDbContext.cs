using AfterMe.Core.Domains.Accounts.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AfterMe.Core.Domains.Accounts.Infrastructures.SqlServer
{
    public class AccountDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<Account>
    {
        public DbSet<Account> Accounts { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Get the connection string from system envars instead of from a config file
            string connString = Environment.GetEnvironmentVariable("AfterMe_Constring");
            if (string.IsNullOrEmpty(connString))
                throw new Exception("Connection string has not been set in system envars.");

            optionsBuilder.UseSqlServer(connString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
