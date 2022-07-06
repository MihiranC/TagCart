using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace TagTeam.Admin.Domain
{
    public class SSRS
    {
        public List<SSRSParameters> Parameters = new List<SSRSParameters>();
        public string Key_ReportPath { get; set; }
        public string Report { get; set; }
        public string Key_ReportServer { get; set; }
        public object ReportOutput { get; set; }
        public string ReportOutputToBase64String { get; set; }
        public string url { get; set; }
    }

    public class SSRSParameters
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
    }
}
