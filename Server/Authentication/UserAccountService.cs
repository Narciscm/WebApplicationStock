using Microsoft.EntityFrameworkCore;
using WebApplicationStock.Server.Data;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Server.Authentication
{
    public class UserAccountService : IUserAccountService //Implement interface
    {
        private readonly DataContext _context;
        private List<UserAccount> _userAccountList;

        //Constructor with param context
        public UserAccountService(DataContext context)
        {
            _context = context;
        }

        //Create a list of users from Db
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
