using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationStock.Client.Services.PersonService;
using WebApplicationStock.Server.Authentication;
using WebApplicationStock.Server.Data;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserAccountService _userAccountService;

        private readonly DataContext _context;

        public AccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        //Get list of all users
        [HttpGet]
        public async Task<IActionResult> ListofUsers()
        {
            var users = await _userAccountService.ListUsers();
            return Ok(users);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]

        //Define a method named Login which will accept LoginRequest and will return a user session object
        public ActionResult<UserSession> Login([FromBody] LoginRequest loginRequest)
        {
            //Create an object
            var jwtAuthenticationManager = new JwtAuthenticationManager(_userAccountService);
            var userSession = jwtAuthenticationManager.GenerateJwtToken(loginRequest.Username, loginRequest.Password);
            if (userSession is null)
                return Unauthorized();
            else
                return userSession;
        }

        //Get user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccount>> GetSingleUser(int id)
        {
            var user = _userAccountService.GetUserAccountById(id);
            if (user == null)
            {
                return NotFound("Sorry, no user here.");
            }
            return Ok(user);
        }

        //Create a user
        [HttpPost]
        public async Task<ActionResult<UserAccount>> CreateUser(UserAccount userAccount)
        {
            userAccount.Role = "Account";
            _userAccountService.AddUser(userAccount);

            return Ok(new List<UserAccount> { userAccount });
        }

        //Update user
        [HttpPut("{id}")]
        public async Task<ActionResult<List<UserAccount>>> UpdateUser(UserAccount userAccount, int id)
        {
            var user = _userAccountService.GetUserAccountById(id);
            if (user == null)
                return NotFound("Sorry, no user here.");
            user.Username= userAccount.Username;
            user.Password= userAccount.Password;

            //await _context.SaveChangesAsync();\
            _userAccountService.UpdateUser(userAccount);

            return Ok(new List<UserAccount> { userAccount });
        }

        //Delete a user
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserAccount>> DeleteUser(int id)
        {
            var user = _userAccountService.GetUserAccountById(id);
            if (user == null)
                return NotFound("Sorry, no user here.");
            _userAccountService.DeleteUser(user);

            return Ok();
        }
    }
}
