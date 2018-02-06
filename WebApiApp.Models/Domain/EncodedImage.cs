namespace WebApiApp.Models.Domain
{
    public class EncodedImage
    {
        public string EncodedImageFile { get; set; }
        public string FileExtension { get; set; }
        public int DeleteId { get; set; }
        public string DeleteImageFile { get; set; }

    }
}