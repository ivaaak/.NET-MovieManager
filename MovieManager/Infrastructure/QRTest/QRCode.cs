using QRCoder;
using System.Drawing;

namespace MovieManager.Infrastructure.QRTest
{
    public class QRCode
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public Bitmap QrCodeImage { get; set; }

        public QRCodeData QrCodeData { get; set; }

        public string TextContent { get; set; }
    }
}
