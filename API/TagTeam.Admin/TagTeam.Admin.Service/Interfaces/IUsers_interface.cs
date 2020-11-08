using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Domain.CustomModels;

namespace TagTeam.Admin.Service.Interfaces
{
    public interface IUsers_interface
    {
        Task<BaseModel> Insert(Users data);
        Task<BaseModel> Update(UpdateData data);
        Task<BaseModel> Delete(Users data);
        Task<BaseModel> Select(int userID, string roleCode);
    }
}




