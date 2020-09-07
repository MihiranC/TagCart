using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain.CustomModels
{
    public class LoginModel
    {
        public string   password { get; set; }
        public string   username { get; set; }
        public string   encryptedpassword { get; set; }
        public string   encryptedusername { get; set; }
        public bool     isvalid { get; set; }

    }
}
