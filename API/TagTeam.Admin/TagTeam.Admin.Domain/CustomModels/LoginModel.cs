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

        public string   confirmepassword { get; set; }
    }

    // for change password

    public class ChangePasswordModel
    {
        public string userInput { get; set; }
        public string resetCode { get; set; }
        public string userID    { get; set; }
        public string password { get; set; }
        public string username { get; set; }


    }

    // for FFrist Sign UP

    public class SignUpModel
    {
        public string userInput { get; set; }
        public string resetCode { get; set; }
        public string userID { get; set; }
        public string password { get; set; }
        public string username { get; set; }


    }
}
