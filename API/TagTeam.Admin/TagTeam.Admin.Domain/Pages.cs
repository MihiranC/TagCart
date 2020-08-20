using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain
{
    public class Pages
    {
        int PageId { get; set; }
        int HeaderId { get; set; }
        string PageName { get; set; }
        string Path { get; set; }
    }
}
