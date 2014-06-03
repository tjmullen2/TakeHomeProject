using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace TakeHomePhotoViewer.PhotoSDK.Models
{
    public class ImageDetailInfo
    {
        private Dictionary<string, string> _imageMetadata; 
        public BitmapImage LargeImage { get; set; }
        public Dictionary<string, string> ImageMetadata { get { return _imageMetadata ?? (_imageMetadata = new Dictionary<string, string>()); } set { _imageMetadata = value; } }
        public string MetadataType { get; set; }
    }
}