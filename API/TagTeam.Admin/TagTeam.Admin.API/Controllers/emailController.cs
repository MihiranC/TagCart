using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Service;

namespace TagTeam.Admin.API.Controllers
{
    [Route("api/TGAdmin/[controller]")]
    [ApiController]
    public class emailController : ControllerBase
    {
        private readonly emailService  _service;

        public emailController(emailService service)
        {
            _service = service;
        }
        [HttpPost("sendEmail")]
        public async Task<ActionResult> sendEmali(MailRequest data)
        {
            var response = await _service.SendEmailAsync(data);
            return Ok(response);
        }
    }
}