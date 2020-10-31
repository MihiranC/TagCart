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
    public class UsersController : ControllerBase
    {
        private readonly IUsers_interface _service;

        public UsersController(IUsers_interface service)
        {
            _service = service;
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert(Users data)
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
        public async Task<ActionResult> Delete(Users data)
        {
            var response = await _service.Delete(data);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<ActionResult> Select(int userID)
        {
            var response = await _service.Select(userID);
            return Ok(response);
        }
    }
}