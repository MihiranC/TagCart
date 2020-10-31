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
    public class SettingsService : ISettings_interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public SettingsService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }

        public async Task<BaseModel> Select(string code)
        {
            try
            {
                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@Code", code, DbType.Int32);
                    var Settings = await connection.QueryAsync<Settings>("TAG_AD_SELECT_Settings", para, commandType: System.Data.CommandType.StoredProcedure);
                    return new BaseModel() { code = "1000", description = "Success", data = Settings };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = code };
            }

        }

        public Settings SelectWithinProject(string code)
        {
            try
            {
                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@Code", code, DbType.String);
                    var Settings = connection.Query<Settings>("TAG_AD_SELECT_Settings", para, commandType: System.Data.CommandType.StoredProcedure) ;
                    return Settings.ToList()[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
