using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace BYSCORE.UI
{
    public class RaffQRCode : IQRCode
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The QRC ode.</returns>
        /// <param name="url">URL.</param>
        /// <param name="pixel">Pixel.</param>
        //public Bitmap GetQRCode(string url, int pixel)
        //{
        //    QRCodeGenerator generator = new QRCodeGenerator();

        //    QRCodeData codeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);

        //    QRCoder.QRCode qrcode = new QRCoder.QRCode(codeData);

        //    Bitmap qrImage = qrcode.GetGraphic(pixel, Color.Black, Color.White, true);

        //    return qrImage;
        //}

        //public MemoryStream GetQRCodeByStream(string url, int pixel)
        //{
        //    Bitmap bitmap = GetQRCode(url, pixel);

        //    MemoryStream memoryStream = new MemoryStream();
        //    bitmap.Save(memoryStream, ImageFormat.Jpeg);
        //    memoryStream.Position = 0;
        //    return memoryStream;
        //}
        public Bitmap GetQRCode(string url, int pixel)
        {
            throw new NotImplementedException();
        }
    }
}
