namespace MovieManager.Data.DataModels
{
    public class QRCodeObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string QrCodeImage { get; set; } //string which can be converted to img

        //public QRCodeData QrCodeData { get; set; }

        public string TextContent { get; set; }
        
        public string PlaylistId { get; set; }

        public Playlist Playlist { get; set; }
    }
}
