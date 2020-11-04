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

        //to user check login
        //select
        [HttpPost("UserCheckLogin")] 
        public async Task<ActionResult> UserCheckLogin(LoginModel loginModel)
        {
            var response = await _service.UserCheckLogin(loginModel);
            return Ok(response);
        }

        //to customer check login
        [HttpPost("CustomerCheckLogin")]
        public async Task<ActionResult> CustomerCheckLogin(LoginModel loginModel)
        {
            var response = await _service.CustomerCheckLogin(loginModel);
            return Ok(response);
        }

        //to User Process First Signup
        //Update
        [HttpPost("UserProcessFirstSignup")]
        public async Task<ActionResult> UserProcessFirstSignup(SignUpModel SignUpModel)
        {
            var response = await _service.UserProcessFirstSignup(SignUpModel);
            return Ok(response);
        }


        //to Customer Process First Signup
        //Update
        [HttpPost("CustomerProcessFirstSignup")]
        public async Task<ActionResult> CustomerProcessFirstSignup(string password, string username)
        {
            var response = await _service.CustomerProcessFirstSignup(password, username);
            return Ok(response);
        }

        //to User Change Password Request 
        //select
        [HttpGet("UserChangePasswordRequest")]
        public async Task<ActionResult> UserChangePasswordRequest(string userInput)
        {
            var response = await _service.UserChangePasswordRequest(userInput);
            return Ok(response);
        }


        //to User Change Password
        //Update
        [HttpPost("UserChangePassword")]
        public async Task<ActionResult> UserChangePassword(ChangePasswordModel ChangePasswordModel)
        {
            var response = await _service.UserChangePassword(ChangePasswordModel);
            return Ok(response);
        }


        //to get User Name passing encrypted user name
        
        [HttpGet("GetUserName")]
        public async Task<ActionResult> GetUserName(string encrpUserName)
        {
            var response = await _service.GetUserName(encrpUserName);
            return Ok(response);
        }


    }
}