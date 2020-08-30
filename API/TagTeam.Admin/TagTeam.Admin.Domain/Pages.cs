using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain
{
    public class Pages
    {
        public int PageId { get; set; }
        public int HeaderId { get; set; }
        public string PageName { get; set; }
        public string Path { get; set; }
    }
}
