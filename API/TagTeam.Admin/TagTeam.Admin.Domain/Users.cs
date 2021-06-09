using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain
{
    public class Users
    {
        public int userID { get; set; }
        public string encryptedUserName { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNo { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string addressLine3 { get; set; }
        public string addressLine4 { get; set; }
        public string email { get; set; }
        public string roleCode { get; set; }

    }
}
