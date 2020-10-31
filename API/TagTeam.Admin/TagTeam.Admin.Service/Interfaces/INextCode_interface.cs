using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.Admin.Domain.CustomModels;

namespace TagTeam.Admin.Service.Interfaces
{
    public interface INextCode_interface
    {
        Task<BaseModel> Select(string prefix);
    }
}
