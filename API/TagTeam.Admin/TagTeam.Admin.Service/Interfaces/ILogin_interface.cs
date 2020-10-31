using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.Admin.Domain.CustomModels;

namespace TagTeam.Admin.Service.Interfaces
{
    public interface ILogin_Interface
    {
        Task<BaseModel> UserCheckLogin(string username , string password);
        Task<BaseModel> CustomerCheckLogin(string username, string password);
        Task<BaseModel> UserProcessFirstSignup(SignUpModel SignUpModel);
        Task<BaseModel> CustomerProcessFirstSignup(string username, string password);
        Task<BaseModel> UserChangePasswordRequest(string userInput);
        Task<BaseModel> GetUserName(string encrpUserName);
        Task<BaseModel> UserChangePassword(ChangePasswordModel ChangePasswordModel);


    }
}
