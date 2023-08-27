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
    }
}
