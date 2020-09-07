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
    public class Ref_DistrictsController : ControllerBase
    {
        private readonly IRef_Districts_interface _service;

        public Ref_DistrictsController(IRef_Districts_interface service)
        {
            _service = service;
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert(Ref_Districts data)
        {
            var response = await _service.Insert(data);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update(UpdateData data)
        {
            var response = await _service.Update(data);
            return Ok(response);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(Ref_Districts data)
        {
            var response = await _service.Delete(data);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<ActionResult> Select(int districtID)
        {
            var response = await _service.Select(districtID);
            return Ok(response);
        }
    }
}