using QRCoder;
using System.Drawing;
using MovieManager.Data.DataModels;

namespace MovieManager.Infrastructure.QRTest
{
    public class QRCodeTest
    {
        QRCode QRCode = new QRCode();

        private void GenerateQRCode(string textInput, EventArgs e)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textInput, QRCodeGenerator.ECCLevel.Q);
           
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);

            using (var ms = new MemoryStream(qrCodeAsBitmapByteArr))
            {
                var qrCodeImage = new Bitmap(ms);
                QRCode.QrCodeImage = qrCodeAsBitmapByteArr;
                QRCode.TextContent = textInput;
            }
        }
    }
}
