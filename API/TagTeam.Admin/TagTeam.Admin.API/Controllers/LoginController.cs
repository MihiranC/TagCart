using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Domain.CustomModels;
using TagTeam.Admin.Service.Interfaces;

namespace TagTeam.Admin.API.Controllers
{
    [Route("api/TGAdmin/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILogin_Interface _service;

        public LoginController(ILogin_Interface service)
        {
            _service = service;
        }

        [HttpGet("Select")]
        public async Task<ActionResult> UserCheckLogin(string username , string password)
        {
            var response = await _service.UserCheckLogin(username, password);
            return Ok(response);
        }

    }
}