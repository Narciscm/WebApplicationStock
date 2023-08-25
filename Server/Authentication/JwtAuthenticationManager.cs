using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Server.Authentication
{
    public class JwtAuthenticationManager
    {
        public const string JWT_SECURITY_KEY = "abcdefghijklmnopq12345sdgfdhjgfhjfg"; //Define a security key
        private const int JWT_TOKEN_VALIDITY_MINS = 20; //Define how many minutes the key is valid

        private IUserAccountService _userAccountService;

        public JwtAuthenticationManager(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        //Create a method to generate the JWT token
        public UserSession? GenerateJwtToken(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrWhiteSpace(password))
                return null;

            //Validate the user credentials
            var userAccount = _userAccountService.GetUserAccountByUsername(userName);
            if (userAccount == null || userAccount.Password != password)
                return null;

            //Generate JWT Token
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAccount.Username),
                new Claim(ClaimTypes.Role, userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials,
            };

            //Create security token object
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            //Return the user session object
            var userSession = new UserSession
            {
                Username = userAccount.Username,
                Role = userAccount.Role,
                Token = token,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };

            return userSession;
        }
    }
}
