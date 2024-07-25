using BusinessLogic.Service.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class CloudinaryService : ICloudinaryService
    {
        private Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public string UploadImage(byte[] imageBytes, string publicId = null)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription("image", new MemoryStream(imageBytes)),
                    PublicId = publicId,
                    Overwrite = true,
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                return uploadResult.SecureUrl.AbsoluteUri; // Return the URL of the uploaded image
            }
            catch (Exception ex)
            {
                // Log exception (ex) here if needed
                throw new InvalidOperationException("Image upload failed", ex);
            }
        }
    }
}
