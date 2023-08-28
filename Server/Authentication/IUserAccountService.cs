using System;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Server.Authentication
{
    public interface IUserAccountService
    {
        //List of users form Db
        Task<List<UserAccount>> ListUsers();

        //Method to get user
        UserAccount? GetUserAccountByUsername(string userName);
    }
}
