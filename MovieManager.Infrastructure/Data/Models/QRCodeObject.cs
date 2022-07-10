namespace MovieManager.Infrastructure.Data.Models
{
    public class QRCodeObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string QrCodeImage { get; set; } //string of the base64 image

        public string TextContent { get; set; }
        
        public string PlaylistId { get; set; }

        public Playlist Playlist { get; set; }
    }
}
