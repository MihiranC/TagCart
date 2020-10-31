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
    public class ImageResizeController : ControllerBase
    {
        private readonly IImageResize_interface _service;

        public ImageResizeController(IImageResize_interface service)
        {
            _service = service;
        }

        [HttpPost("Resize")]
        public async Task<ActionResult> Insert(ImageResize imageResize)
        {
            var response = await _service.Resize(imageResize);
            return Ok(response);
        }
    }
}