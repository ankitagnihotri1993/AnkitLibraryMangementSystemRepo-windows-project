using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSDAL.Entity.Master.User
{
   public class userEntity
    {
        public int uid { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Roleid { get; set; }
        public string Rolename { get; set; }
    }
}
