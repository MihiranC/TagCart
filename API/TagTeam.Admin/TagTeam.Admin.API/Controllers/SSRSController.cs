using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagTeam.Admin.Service.Interfaces;
using TagTeam.Admin.Domain;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using TagTeam.Admin.Domain.CustomModels;

namespace TagTeam.Admin.API.Controllers
{
    [Route("api/TGAdmin/[controller]")]
    [ApiController]
    public class SSRSController : ControllerBase
    {

        [HttpPost("DownloadSSRS")]
        public BaseModel DownloadSSRS(SSRS SSRSDetails)
        {
            string bytelength = "";
            string URL = "";
            try
            {
                //Warning[] warnings;
                //string[] streamIds;
                //string mimeType = string.Empty;
                //string encoding = string.Empty;
                //string extension = string.Empty;

                //ReportViewer RV = new ReportViewer();

                //RV.ProcessingMode = ProcessingMode.Remote;
                //RV.ServerReport.ReportServerUrl = new Uri(SSRSDetails.Key_ReportServer);
                //RV.ServerReport.ReportPath = "/" + SSRSDetails.Key_ReportPath + "/" + SSRSDetails.Report;

                //string x = RV.ServerReport.ReportServerUrl + RV.ServerReport.ReportPath;

                //List<ReportParameter> paramaters = new List<ReportParameter>();

                //for (int i = 0; i < SSRSDetails.Parameters.Count; i++)
                //{
                //    paramaters.Add(new ReportParameter(SSRSDetails.Parameters[i].ParameterName, SSRSDetails.Parameters[i].ParameterValue));
                //}

                //RV.ServerReport.SetParameters(paramaters);

                //SSRSDetails.ReportOutput = RV.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                //SSRSDetails.ReportOutputToBase64String = Convert.ToBase64String(SSRSDetails.ReportOutput);

                //List<SSRS> reprot = new List<SSRS>();
                //reprot.Add(SSRSDetails);

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

                //http://win-grpgfkgev5i/reportserver/?/WeddingEkataShopping/test&rs:Command=Render&rs:Format=PDF

                URL = SSRSDetails.Key_ReportServer + "/?/" + SSRSDetails.Key_ReportPath + "/" + SSRSDetails.Report;
                string Command = "Render";
                string Format = "PDF";
                string parameters = "";
                for (int i = 0; i < SSRSDetails.Parameters.Count; i++)
                {
                    parameters = parameters + "&" + SSRSDetails.Parameters[i].ParameterName + "=" + SSRSDetails.Parameters[i].ParameterValue;
                }

                if (SSRSDetails.Parameters.Count == 0)
                {
                    URL = URL + "&rs:Command=" + Command + "&rs:Format=" + Format;
                }
                else
                {
                    URL = URL + parameters + "&rs:Command=" + Command + "&rs:Format=" + Format;
                }
                SSRSDetails.url = URL;
                System.Net.HttpWebRequest Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);

                Req.Credentials = System.Net.CredentialCache.DefaultCredentials;
                Req.Method = "GET";
                System.Net.WebResponse objResponse = Req.GetResponse();
                System.IO.Stream stream = objResponse.GetResponseStream();

                byte[] data; // will eventually hold the result
                             // create a MemoryStream to build the result
                using (var mstrm = new MemoryStream())
                {
                    using (var s = objResponse.GetResponseStream())
                    {
                        var tempBuffer = new byte[4096];
                        int bytesRead;
                        while ((bytesRead = s.Read(tempBuffer, 0, tempBuffer.Length)) != 0)
                        {
                            mstrm.Write(tempBuffer, 0, bytesRead);
                        }
                    }
                    mstrm.Flush();
                    data = mstrm.GetBuffer();
                }

                var filedata = new FileContentResult(data, "application/pdf")
                {
                    FileDownloadName = SSRSDetails.Report
                };
                

                SSRSDetails.ReportOutput = filedata;

                SSRSDetails.ReportOutputToBase64String = Convert.ToBase64String(data);

                return new BaseModel() { code = "1000", description = "Success", data = SSRSDetails };

            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = SSRSDetails };
            }
        }





    }
}