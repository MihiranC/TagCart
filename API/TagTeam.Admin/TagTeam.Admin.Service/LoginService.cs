using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Domain.CustomModels;
using TagTeam.Admin.Service.Interfaces;

namespace TagTeam.Admin.Service
{
    public class LoginService : ILogin_Interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public LoginService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }

        public async Task<BaseModel> UserCheckLogin(string username , string password)
        {
            try
            {
                EncryptionService encryption = new EncryptionService();

                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    var EncryptedUserName = encryption.ReturnEncryptedUserName(username);
                    var EncryptedPassword = encryption.ReturnEncryptedPassword(username,password);
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@EncryptedUserName", EncryptedUserName, DbType.String);
                    para.Add("@EncryptedPassword", EncryptedPassword, DbType.String);
                    var Verified_UserCredentials = await connection.QueryAsync<LoginModel>("Tag_AD_Login_SelectAccountDetails", para, commandType: System.Data.CommandType.StoredProcedure);

                    BaseModel BaseModelObj = new BaseModel();
                    if ((Verified_UserCredentials).ToList()[0].isvalid)
                    {
                        BaseModelObj.code = "1000";
                        BaseModelObj.description = "Success";
                        BaseModelObj.data = Verified_UserCredentials;
                    }
                    else
                    {

                        BaseModelObj.code = "999";
                        BaseModelObj.description = "Username or password is incorrect";
                        BaseModelObj.data = Verified_UserCredentials;
                    }

                    return BaseModelObj;

                   // encryption.ReturnEncryptedPassword  = 
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = username };
            }

        }

    }
}
