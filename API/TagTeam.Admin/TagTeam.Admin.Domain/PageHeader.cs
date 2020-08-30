using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain
{
    public class PageHeader
    {
        public int HeaderId { get; set; }
        public string HeaderName { get; set; }
        public string Icon { get; set; }
        public List<Pages> pages{ get; set; }
    }
}
