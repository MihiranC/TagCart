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
    public class PagesService : IPages_Interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public PagesService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

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
                    List<PageHeader> pgList = new List<PageHeader>();
                    pgList = pageHeader.ToList();
                    for(int i= 0; i < pgList.Count; i++)
                    {
                        DynamicParameters pagesPara = new DynamicParameters();
                        pagesPara.Add("@UserID", userId, DbType.Int16);
                        pagesPara.Add("@Type", "PG", DbType.String);
                        pagesPara.Add("@HeaderID", pgList[i].HeaderId, DbType.Int16);
                        pgList[i].pages = (await connection.QueryAsync<Pages>("TAG_AD_SELECT_UserWisePages", pagesPara, commandType: System.Data.CommandType.StoredProcedure)).ToList();

                    }

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
