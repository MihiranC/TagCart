using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.Admin.Domain.CustomModels;

namespace TagTeam.Admin.Service.Interfaces
{
    public interface ITest_Interface
    {
        Task<BaseModel> Select();
    }
}
