using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationStock.Shared
{
    public class UserSession
    {
        //Class with prop to for user session
        public string Username { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public int ExpiresIn { get; set; } //store the number of seconds remaining for token
        public DateTime ExpiryTimeStamp { get; set; }
    }
}
