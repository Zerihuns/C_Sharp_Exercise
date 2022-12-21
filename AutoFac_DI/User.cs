using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFac_DI
{
    internal class User
    {
        public User(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}
