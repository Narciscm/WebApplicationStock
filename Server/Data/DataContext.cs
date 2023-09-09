using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Server.Data
{
    //Create Database
    public class DataContext:DbContext //Implement DbContext
    {
        //Create a constructor for DataContext
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        //Create a DbSet prop to use UserAccounts list
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
