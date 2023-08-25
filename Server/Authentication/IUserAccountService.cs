using System;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Server.Authentication
{
    public interface IUserAccountService
    {
        Task<List<UserAccount>> ListUsers();

        UserAccount? GetUserAccountByUsername(string userName);
    }
}
