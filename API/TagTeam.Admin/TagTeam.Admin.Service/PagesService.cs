using Dapper;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Domain.CustomModels;
using TagTeam.Admin.Service.Interfaces;

namespace TagTeam.Admin.Service
{
    public class PagesService : IPages_Interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sPConnectionString;

        public PagesService(string adminConnectionString, string sPConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sPConnectionString = sPConnectionString;

        }

        public async Task<BaseModel> Select(int userId)
        {
            try
            {                
                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@UserID", userId, DbType.Int16);
                    para.Add("@Type", "PH", DbType.String);
                    var pageHeader =  await connection.QueryAsync<PageHeader>("TAG_AD_SELECT_UserWisePages", para, commandType: System.Data.CommandType.StoredProcedure);
                    BaseModel bm = new BaseModel();
                    bm.data = pageHeader;
                    return new BaseModel() { code = "1000", description = "Success", data = pageHeader };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = userId };
            }

        }
    }
}
