namespace MovieManager.Api.ApiServices.Models
{
    public class QRCodeObject
    {
        public string Id { get; set; }

        public string QrCodeImage { get; set; }

        public string TextContent { get; set; }
        
        public string PlaylistId { get; set; }

        public Playlist Playlist { get; set; }
    }
}
