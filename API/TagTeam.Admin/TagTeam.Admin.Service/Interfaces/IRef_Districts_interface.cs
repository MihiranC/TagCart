using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Domain.CustomModels;
namespace TagTeam.Admin.Service.Interfaces
{
    public interface IRef_Districts_interface
    {
        Task<BaseModel> Insert(Ref_Districts data);
        Task<BaseModel> Update(UpdateData data);
        Task<BaseModel> Delete(Ref_Districts data);
    }
}
