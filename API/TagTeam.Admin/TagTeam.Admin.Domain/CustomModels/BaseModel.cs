using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain.CustomModels
{
    public class BaseModel
    {
        public string code { get; set; }
        public string description { get; set; }
        public object data { get; set; }
    }
}
