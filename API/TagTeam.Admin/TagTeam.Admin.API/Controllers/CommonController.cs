using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TagTeam.Admin.API.Controllers
{
    [Route("api/TGAdmin/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        [HttpGet("DownloadThisFile")]
        public async Task<IActionResult> DownloadthisFile(string file, string contentType, string filename)
        {
            byte[] byteArr = System.IO.File.ReadAllBytes(file);
            string mimeType = contentType;
            try
            {
                var filedata = new FileContentResult(byteArr, mimeType)
                {
                    FileDownloadName = filename
                };
                return filedata;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}