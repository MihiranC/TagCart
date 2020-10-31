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
    public class SettingsController : ControllerBase
    {
        private readonly ISettings_interface _service;

        public SettingsController(ISettings_interface service)
        {
            _service = service;
        }

        [HttpPost("Select")]
        public async Task<ActionResult> Insert(string code)
        {
            var response = await _service.Select(code);
            return Ok(response);
        }
    }
}