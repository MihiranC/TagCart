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
    public class Ref_DistrictsService
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public Ref_DistrictsService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }

        public async Task<BaseModel> Insert(Ref_Districts districts)
        {
            try
            {
                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(districts);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "I", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Districts]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = districts };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = districts };
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

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Districts]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = data };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = data };
            }
        }

        public async Task<BaseModel> Delete(Ref_Districts districts)
        {
            try
            {
                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    string JsonData = JsonConvert.SerializeObject(districts);
                    para.Add("@JsonData", JsonData, DbType.String);
                    para.Add("@Action", "D", DbType.String);

                    await connection.ExecuteAsync("[dbo].[TAG_AD_POPULATE_Districts]", para, commandType: System.Data.CommandType.StoredProcedure);

                    return new BaseModel() { code = "1000", description = "Success", data = districts };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = districts };
            }

        }
    }
}
