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
    public class NextCodeService : INextCode_interface                                                               
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public NextCodeService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }

        public async Task<BaseModel> Select(string prefix)
        {
            try
            {
                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@Prefix", prefix, DbType.String);
                    var Districts = await connection.QueryAsync<NextCode>("TAG_AD_SELECT_NextNumber", para, commandType: System.Data.CommandType.StoredProcedure);
                    return new BaseModel() { code = "1000", description = "Success", data = Districts };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = prefix };
            }

        }
    }
}
