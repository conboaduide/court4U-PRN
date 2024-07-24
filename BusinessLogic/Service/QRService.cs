using BusinessLogic.Service.Interface;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class QRService : IQRService
    {
        public QRService() { }

        public string GenerateQRCode(string id)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(id, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new Base64QRCode(qrCodeData);
            var qrCodeBitmap = qrCode.GetGraphic(20);
            return qrCodeBitmap;
        }
    }
}
