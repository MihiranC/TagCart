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



        //to User Check Login
        public async Task<BaseModel> UserCheckLogin(LoginModel loginModel)
        {
            try
            {
                EncryptionService encryption = new EncryptionService();

                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    var EncryptedUserName = encryption.ReturnEncryptedUserName(loginModel.username);
                    var EncryptedPassword = encryption.ReturnEncryptedPassword(loginModel.username, loginModel.password);
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@EncryptedUserName", EncryptedUserName, DbType.String);
                    para.Add("@EncryptedPassword", EncryptedPassword, DbType.String);
                    para.Add("@Type", "U", DbType.String);

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
                return new BaseModel() { code = "998", description = ex.Message, data = loginModel };
            }

        }


        //to Customer Check Login
        public async Task<BaseModel> CustomerCheckLogin(LoginModel loginModel)
        {
            try
            {
                EncryptionService encryption = new EncryptionService();

                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    var EncryptedUserName = encryption.ReturnEncryptedUserName(loginModel.username);
                    var EncryptedPassword = encryption.ReturnEncryptedPassword(loginModel.username, loginModel.password);
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@EncryptedUserName", EncryptedUserName, DbType.String);
                    para.Add("@EncryptedPassword", EncryptedPassword, DbType.String);
                    para.Add("@Type", "C", DbType.String);

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
                return new BaseModel() { code = "998", description = ex.Message, data = loginModel };
            }

        }



        // to User Process First Signup - For USERS
        public async Task<BaseModel> UserProcessFirstSignup(SignUpModel SignUpModel)
        {
            try
            {
                EncryptionService encryption = new EncryptionService();

                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    var EncryptedUserName = encryption.ReturnEncryptedUserName(SignUpModel.username);
                    var EncryptedPassword = encryption.ReturnEncryptedPassword(SignUpModel.password, SignUpModel.username);
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@EncryptedUserName", EncryptedUserName, DbType.String);
                    para.Add("@EncryptedPassword", EncryptedPassword, DbType.String);
                    para.Add("@Type", "U", DbType.String);

                    await connection.ExecuteAsync("[dbo].[Tag_AD_Login_ProcessFirstSignUp]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = SignUpModel };
                }

            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = SignUpModel };
            }

        }


        // to Customer Process First Signup - For CUSTOMERS
        public async Task<BaseModel> CustomerProcessFirstSignup(string password, string username)
        {
            try
            {
                EncryptionService encryption = new EncryptionService();

                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    var EncryptedUserName = encryption.ReturnEncryptedUserName(username);
                    var EncryptedPassword = encryption.ReturnEncryptedPassword(password, username);
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@EncryptedUserName", EncryptedUserName, DbType.String);
                    para.Add("@EncryptedPassword", EncryptedPassword, DbType.String);
                    para.Add("@Type", "C", DbType.String);

                    await connection.ExecuteAsync("[dbo].[Tag_AD_Login_ProcessFirstSignUp]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = username };
                }

            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = username };
            }

        }



        // to User get UserName with passing encrypted username
        public async Task<BaseModel> GetUserName(string encrpUserName)
        {
            try
            {


                using (var connection = new SqlConnection(_adminConnectionString))
                {

                    DynamicParameters para = new DynamicParameters();
                    para.Add("@encrypUserName", encrpUserName, DbType.String);
                    para.Add("@Type", "U", DbType.String);


                    var Result = await connection.QueryAsync<ChangePasswordModel>("[dbo].[Tag_AD_GetUserName]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = Result };
                }

            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = encrpUserName };
            }

        }


        // to User Change Password Request
        public async Task<BaseModel> UserChangePasswordRequest(string userInput)
        {
            try
            {
                

                using (var connection = new SqlConnection(_adminConnectionString))
                {

                    DynamicParameters para = new DynamicParameters();
                    para.Add("@UserInput", userInput, DbType.String);
                    para.Add("@Type", "U", DbType.String);

                    
                    var UserID = await connection.QueryAsync<ChangePasswordModel>("[dbo].[Tag_AD_Login_ChangePasswordRequest]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = UserID };
                }

            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = userInput };
            }

        }


        // to User Change Password
        public async Task<BaseModel> UserChangePassword(ChangePasswordModel ChangePasswordModel)
        {
            try
            {
                EncryptionService encryption = new EncryptionService();

                using (var connection = new SqlConnection(_adminConnectionString))
                {

                    DynamicParameters para = new DynamicParameters();

                    //string JsonData = JsonConvert.SerializeObject(ChangePasswordModel);
                    var EncryptedPassword = encryption.ReturnEncryptedPassword(ChangePasswordModel.password, ChangePasswordModel.username);

                    para.Add("@UserID", ChangePasswordModel.userID, DbType.String);
                    para.Add("@ResetCode", ChangePasswordModel.resetCode, DbType.String);
                    para.Add("@UserName", ChangePasswordModel.username, DbType.String);
                    para.Add("@EncryptedPassword", EncryptedPassword, DbType.String);
                    para.Add("@Type", "C", DbType.String);


                    var UserID = await connection.QueryAsync<ChangePasswordModel>("[dbo].[Tag_AD_Login_ChangePassword]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = ChangePasswordModel.userID };
                }

            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = ChangePasswordModel.userID };
            }

        }

    }
}
