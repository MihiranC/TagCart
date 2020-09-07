using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain.CustomModels
{
    public class UpdateData
    {
        public int UserID { get; set; }
        public JObject NewData { get; set; }
        public JObject OldData { get; set; }
    }
}
