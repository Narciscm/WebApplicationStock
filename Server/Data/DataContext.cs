using Microsoft.EntityFrameworkCore;
using System;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Server.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<UserAccount>? UserAccounts { get; set; }
    }
}
