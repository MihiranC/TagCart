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
    public class NextCodeController : ControllerBase
    {
        private readonly INextCode_interface _service;

        public NextCodeController(INextCode_interface service)
        {
            _service = service;
        }

        [HttpGet("Select")]
        public async Task<ActionResult> Select(string prefix)
        {
            var response = await _service.Select(prefix);
            return Ok(response);
        }
    }
}