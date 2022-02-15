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
    public class Ref_CitiesService : IRef_Cities_interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public Ref_CitiesService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }


        public async Task<BaseModel> Select(int cityID, int districtID)
        {
            try
            {
                using (var connection = new SqlConnection(_adminConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@CityID", cityID , DbType.Int32);
                    para.Add("@DistrictID", districtID, DbType.Int32);
                    var Cities = await connection.QueryAsync<Ref_Cities>("Tag_AD_SELECT_Ref_Cities", para, commandType: System.Data.CommandType.StoredProcedure);
                    return new BaseModel() { code = "1000", description = "Success", data = Cities };
                }
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = cityID };
            }

        }
    }
}
