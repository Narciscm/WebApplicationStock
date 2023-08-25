using Microsoft.EntityFrameworkCore;
using WebApplicationStock.Server.Data;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Server.Authentication
{
    public class UserAccountService : IUserAccountService
    {
        private readonly DataContext _context;
        private List<UserAccount> _userAccountList;

        public UserAccountService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<UserAccount>> ListUsers()
        {
            var users = await (from user in _context.UserAccounts select new UserAccount()
            {
                Id = user.Id,
                Username=user.Username, Password=user.Password, Role=user.Role
            }).ToListAsync();

            return users;
        }

        public UserAccount? GetUserAccountByUsername(string userName)
        {
            if (_userAccountList == null) 
            {
                _userAccountList = ListUsers().Result;    
            }

            return _userAccountList.FirstOrDefault(x => x.Username == userName);
        }
    }
}
