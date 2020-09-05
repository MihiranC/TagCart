using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain.CustomModels
{
    public class UpdateData
    {
        public int UserID { get; set; }
        public string NewData { get; set; }
        public string OldData { get; set; }
    }
}
