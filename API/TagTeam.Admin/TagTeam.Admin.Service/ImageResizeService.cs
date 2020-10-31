using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Domain.CustomModels;
using TagTeam.Admin.Service.Interfaces;

namespace TagTeam.Admin.Service
{
    public class ImageResizeService :  IImageResize_interface
    {
        private readonly string _adminConnectionString;
        private readonly string _sCConnectionString;

        public ImageResizeService(string adminConnectionString, string sCConnectionString)
        {
            _adminConnectionString = adminConnectionString;
            _sCConnectionString = sCConnectionString;

        }

        public async Task<BaseModel> Resize(ImageResize imageResize)
        {
            try
            {
                string convertedImageData = imageResize.originalImage.Substring(imageResize.originalImage.LastIndexOf(',') + 1);
                byte[] image64 = Convert.FromBase64String(convertedImageData);

                using (var ms = new MemoryStream(image64))
                {
                    var image = Image.FromStream(ms);
                    
                    var width = (int)(imageResize.width);
                    var height = (int)(imageResize.height);

                    var newImage = new Bitmap(width, height);
                    Graphics.FromImage(newImage).DrawImage(image, 0, 0, width, height);
                    Bitmap bmp = new Bitmap(newImage);

                    using (MemoryStream returnms = new MemoryStream())
                    {
                        bmp.Save(returnms,ImageFormat.Jpeg);
                        imageResize.resizedImage = "data:image/jpeg;base64," + Convert.ToBase64String(returnms.ToArray());
                    }
                    return new BaseModel() { code = "1000", description = "Success", data = imageResize };
                }


            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = imageResize };
            }

        }
    }
}
