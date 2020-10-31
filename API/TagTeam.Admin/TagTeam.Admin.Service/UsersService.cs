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
    public class UsersService : IUsers_interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public UsersService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }

        public async Task<BaseModel> Insert(Users users)
        {
            try
            {

                EncryptionService encryption = new EncryptionService();

                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    users.encryptedUserName = encryption.ReturnEncryptedUserName(users.username);
                    string JsonData = JsonConvert.SerializeObject(users);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "I", DbType.String);
                    para.Add("@RoleCode", "U", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Users]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = users };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = users };
            }

        }

        public async Task<BaseModel> Update(UpdateData data)
        {
            try
            {
                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(data.NewData);
                    string OldJsonData = JsonConvert.SerializeObject(data.OldData);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@OldJsonData", OldJsonData, DbType.String);
                    para.Add("@Action", "U", DbType.String);
                    para.Add("@RoleCode", "U", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Users]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = data };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = data };
            }
        }

        public async Task<BaseModel> Delete(Users users)
        {
            try
            {
                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(users);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "D", DbType.String);
                    para.Add("@RoleCode", "U", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Users]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = users };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = users };
            }
        }

        public async Task<BaseModel> Select(int userID)
        {
            try
            {
                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@UserID", userID, DbType.Int32);
                    var Districts = await connection.QueryAsync<Users>("TAG_AD_SELECT_Users", para, commandType: System.Data.CommandType.StoredProcedure);
                    return new BaseModel() { code = "1000", description = "Success", data = Districts };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = userID };
            }

        }
    }
}
