using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain
{
    public class Settings
    {
        public int SettingID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsEnable { get; set; }
        public string Value { get; set; }
    }
}
