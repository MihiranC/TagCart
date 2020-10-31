using System;
using System.Collections.Generic;
using System.Text;

namespace TagTeam.Admin.Domain.CustomModels
{
    public class ImageResize
    {
        public string originalImage { get; set; }
        public string resizedImage { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
