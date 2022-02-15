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
    public class Ref_CitiesController : ControllerBase
    {
        private readonly IRef_Cities_interface _service;

        public Ref_CitiesController(IRef_Cities_interface service)
        {
            _service = service;
        }

        [HttpGet("Select")]
        public async Task<ActionResult> Select(int cityID, int districtID)
        {
            var response = await _service.Select(cityID, districtID);
            return Ok(response);
        }
    }
}