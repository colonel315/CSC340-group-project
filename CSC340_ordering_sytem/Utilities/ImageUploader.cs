using System.IO;
using System.Linq;
using System.Web;
using static System.Web.Hosting.HostingEnvironment;

namespace CSC340_ordering_sytem.Utilities
{
    public class ImageUploader
    {
        private static readonly string[] ImageTypes = {
            "image/gif",
            "image/jpeg",
            "image/pjpeg",
            "image/png"
        };

        private static readonly string UploadDirectory = "~/Content/Uploads";

        public static bool IsValidImageType(HttpPostedFileBase image)
        {
            return ImageTypes.Contains(image.ContentType);
        }

        public static string Upload(HttpPostedFileBase image)
        {
            var newFileName = SHA256Hasher.Create(image.FileName);
            var extension = Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(path1: MapPath(UploadDirectory), path2: newFileName);
            var imageUrl = Path.Combine(UploadDirectory, newFileName + extension);
            image.SaveAs(imagePath + extension);
            return imageUrl;
        }
    }
}