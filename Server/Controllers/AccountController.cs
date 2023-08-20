using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationStock.Server.Authentication;
using WebApplicationStock.Server.Data;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserAccountService _userAccountService;

        private readonly DataContext _context;

        public AccountController(UserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userAccountService.List();
            return Ok(users);
        }

        [HttpPost]
        [Route("Login")]
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
