using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagTeam.Admin.Service.Interfaces;


namespace TagTeam.Admin.API.Controllers
{
    [Route("api/TGAdmin/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPages_Interface _service;

        public PagesController(IPages_Interface service)
        {
            _service = service;
        }

        [HttpGet("Select")]
        public async Task<ActionResult> Select(int userId)
        {
            var response = await _service.Select(userId);
            return Ok(response);
        }
    }
}