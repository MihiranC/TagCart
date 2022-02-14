using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagTeam.Admin.Service.Interfaces;
using TagTeam.Admin.Domain;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace TagTeam.Admin.API.Controllers
{
    [Route("api/TGAdmin/[controller]")]
    [ApiController]
    public class SSRSController : ControllerBase
    {

        [HttpPost("DownloadSSRS")]
        public SSRS DownloadSSRS(SSRS SSRSDetails)
        {
            try
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportViewer RV = new ReportViewer();

                RV.ProcessingMode = ProcessingMode.Remote;
                RV.ServerReport.ReportServerUrl = new Uri(SSRSDetails.Key_ReportServer);
                RV.ServerReport.ReportPath = "/" + SSRSDetails.Key_ReportPath + "/" + SSRSDetails.Report;

                string x = RV.ServerReport.ReportServerUrl + RV.ServerReport.ReportPath;

                List<ReportParameter> paramaters = new List<ReportParameter>();

                for (int i = 0; i < SSRSDetails.Parameters.Count; i++)
                {
                    paramaters.Add(new ReportParameter(SSRSDetails.Parameters[i].ParameterName, SSRSDetails.Parameters[i].ParameterValue));
                }

                RV.ServerReport.SetParameters(paramaters);

                SSRSDetails.ReportOutput = RV.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                SSRSDetails.ReportOutputToBase64String = Convert.ToBase64String(SSRSDetails.ReportOutput);

                List<SSRS> reprot = new List<SSRS>();
                reprot.Add(SSRSDetails);

                //return result;

                //HttpResponseMessage responseSSRS = new HttpResponseMessage(HttpStatusCode.OK);
               
                //    responseSSRS.Content = new StreamContent(new MemoryStream(SSRSDetails.ReportOutput));
                //    responseSSRS.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                //    responseSSRS.Content.Headers.ContentLength = SSRSDetails.ReportOutput.Length;
                //    ContentDispositionHeaderValue contentDisposition = null;
                //    if (ContentDispositionHeaderValue.TryParse("inline; filename=test.pdf", out contentDisposition))
                //    {
                //        responseSSRS.Content.Headers.ContentDisposition = contentDisposition;
                //    }

                return SSRSDetails;

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}