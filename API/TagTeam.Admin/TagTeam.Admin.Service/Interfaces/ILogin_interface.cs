using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.Admin.Domain.CustomModels;

namespace TagTeam.Admin.Service.Interfaces
{
    public interface ILogin_Interface
    {
        Task<BaseModel> UserCheckLogin(LoginModel loginModel);
        Task<BaseModel> CustomerCheckLogin(LoginModel loginModel);
        Task<BaseModel> UserProcessFirstSignup(SignUpModel SignUpModel);
        Task<BaseModel> CustomerProcessFirstSignup(SignUpModel SignUpModel);
        Task<BaseModel> UserChangePasswordRequest(string userInput);
        Task<BaseModel> CustomerChangePasswordRequest(string userInput);
        Task<BaseModel> GetUserName(string encrpUserName);
        Task<BaseModel> UserChangePassword(ChangePasswordModel ChangePasswordModel);
        Task<BaseModel> CustomerChangePassword(ChangePasswordModel ChangePasswordModel);


    }
}
