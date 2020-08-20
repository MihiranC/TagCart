using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain.CustomModels
{
    public class PagesWithHeader
    {
        string headerName { get; set; }
        string icon { get; set; }
        Pages pages { get; set; }
    }
}
