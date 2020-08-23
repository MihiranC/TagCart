using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Domain.CustomModels;
using TagTeam.Admin.Service.Interfaces;

namespace TagTeam.Admin.Service
{
    public class TestService : ITest_Interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sPConnectionString;

        public TestService(string adminConnectionString, string sPConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sPConnectionString = sPConnectionString;

        }

        public async Task<BaseModel> Select()
        {
            try
            {

                //using (var connection = new SqlConnection(_connectionString))
                //{
                //    DynamicParameters para = new DynamicParameters();
                //    string JsonData = JsonConvert.SerializeObject(null);
                //    para.Add("@JsonData", JsonData, DbType.String);
                //    para.Add("@Operation", "I", DbType.String);
                //    para.Add("@SkipApproval", 0, DbType.Boolean);

                //    await connection.ExecuteAsync("[cal].[InsertCallCenterOfficer]", para, commandType: System.Data.CommandType.StoredProcedure);

                //}
                List<Test> lt = new List<Test>();
                Test test = new Test();
                test.testValue1 = "Test Data 1";
                test.testValue2 = "Test Data 2";
                Test test1 = new Test();
                test1.testValue1 = "Test Data 1";
                test1.testValue2 = "Test Data 2";

                lt.Add(test);
                lt.Add(test1);


                return new BaseModel() { code = "1000", description = "Success", data = lt };
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = null };
            }

        }
    }
}

//mihiran Testing