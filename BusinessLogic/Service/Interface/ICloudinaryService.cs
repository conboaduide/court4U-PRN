using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Interface
{
    public interface ICloudinaryService
    {
        string UploadImage(byte[] imageBytes, string publicId = null);
    }
}
