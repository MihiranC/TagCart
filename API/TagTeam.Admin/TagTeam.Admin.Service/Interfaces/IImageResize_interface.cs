﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Domain.CustomModels;

namespace TagTeam.Admin.Service.Interfaces
{
    public interface IImageResize_interface
    {
        Task<BaseModel> Resize(ImageResize imageResize);
    }
}
