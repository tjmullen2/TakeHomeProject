using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TakeHomePhotoViewer.ImgurAPI;
using TakeHomePhotoViewer.ImgurAPI.Models;
using TakeHomePhotoViewer.PhotoSDK.Models;

namespace TakeHomePhotoViewer.PhotoSDK.Repositories
{
    public class ImgurViralRepository : IImageRepository
    {
        private ObservableCollection<ImageSnapshotInfo> _cachedImages;
        public ObservableCollection<ImageSnapshotInfo> CachedImages { get { return _cachedImages ?? (_cachedImages = new ObservableCollection<ImageSnapshotInfo>()); } set { _cachedImages = value; } }

        private ImgurClient _client = new ImgurClient("3fdb44d4988fa3b", "");


        public ImgurViralRepository()
        {
            CachedImages.Clear();
            _client.GetMainGalleryImages(ImgurGallerySection.Hot, ImgurGallerySort.Viral, 0, (s) =>
            {
                ImgurImageData data = s;
                Debug.WriteLine(s.Images.First().AccountUrl);
                foreach (var imgurImage in data.Images)
                {
                    var newImage = new ImageSnapshotInfo();
                    // append "t" to ID get a thumbnail according to the api 
                    newImage.ImageUrl = imgurImage.Link;
                    newImage.Id = imgurImage.ID;
                    newImage.IsWebBasedSource = true;
                    newImage.SourceId = "ImgurViral";
                    newImage.ThumbnailUrl = imgurImage.Link.Replace(imgurImage.ID, imgurImage.ID + "t");
                    CachedImages.Add(newImage);
                }
            });
        }

        public string GetRepositorySourceId()
        {
            return "ImgurViral";
        }

        public async Task<IEnumerable<ImageSnapshotInfo>> GetImagesFromRepositoryAsync(int startIndex, int imageCount)
        {
            var images = new List<ImageSnapshotInfo>();
            int currentIndex = 0;
            foreach (var image in CachedImages)
            {
                if (currentIndex >= startIndex)
                    images.Add(image);
                if (images.Count == imageCount)
                    break;
                currentIndex++;
            }
            return await Task.FromResult(images);
        }

        public Task<int> GetAvailableImagesFromRepositoryAsync()
        {
            return Task.FromResult(CachedImages.Count);
        }

        public async Task<ImageDetailInfo> GetImageAndMetadataAsync(string imageId)
        {
            foreach (var image in CachedImages)
            {
                if (image.Id == imageId)
                {
                    var result = new ImageDetailInfo();
                    result.LargeImageUrl = image.ImageUrl;
                    result.MetadataType = "ImgurImage";
                    // TODO: Download image and metadata and compose Metadata dictionary
                    return result;
                }
            }
            return null;
        }

        public Task<int> GetImageCountAsync()
        {
            return Task.FromResult(CachedImages.Count);
        }

        public Task<BitmapImage> GetThumbnail(string imageId, int maxWidth, int maxHeight, bool fillMax)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetThumbnailUrlAsync(string imageId, int maxWidth, int maxHeight, bool fillMax)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetImageUrlAsync(string imageId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsWebBasedAsync()
        {
            return Task.FromResult(true);
        }
    }
}