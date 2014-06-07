using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

        private readonly ImgurClient _client = new ImgurClient("3fdb44d4988fa3b", "");


        public ImgurViralRepository()
        {
            CachedImages.Clear();
            InitializeRepositoryData();
        }

        private async void InitializeRepositoryData()
        {
            var data = await _client.GetMainGalleryImages(ImgurGallerySection.Hot, ImgurGallerySort.Viral, 0);

            foreach (var imgurImage in data.Images)
            {
                CachedImages.Add(new ImageSnapshotInfo(imgurImage));
            }

            OnRepositoryCollectionChanged(this, null);
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

        public event EventHandler RepositoryCollectionChanged;

        private void OnRepositoryCollectionChanged(object sender, EventArgs e)
        {
            EventHandler collectionChanged = RepositoryCollectionChanged;
            if (collectionChanged != null)
                collectionChanged(this, e);
        }
    }
}

namespace TakeHomePhotoViewer.PhotoSDK.Models
{
    public partial class ImageSnapshotInfo
    {
        // Custom constructor implementation
        public ImageSnapshotInfo(ImgurSingleImage imageInfo)
        {
            ImageMetadata = new Dictionary<string, string>();
            var imgurImageInfo = imageInfo.Image;

            Id = imgurImageInfo.ID;
            IsWebBasedSource = true;
            SourceId = "ImgurViral";
            ImageUrl = imgurImageInfo.Link;
            ThumbnailUrl = imgurImageInfo.Link.Replace(imgurImageInfo.ID, imgurImageInfo.ID + "t");

            ImageMetadata.Add("height", imgurImageInfo.Height.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("id", imgurImageInfo.ID);
            ImageMetadata.Add("isAnimated", imgurImageInfo.IsAnimated.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("link", imgurImageInfo.Link);
            ImageMetadata.Add("score", imgurImageInfo.Score.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("size", imgurImageInfo.Size.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("title", imgurImageInfo.Title);
            ImageMetadata.Add("type", imgurImageInfo.Type);
            ImageMetadata.Add("ups", imgurImageInfo.Ups.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("views", imgurImageInfo.Views.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("width", imgurImageInfo.Width.ToString(CultureInfo.InvariantCulture));
        }
        // Custom constructor implementation
        public ImageSnapshotInfo(ImgurImage imgurImageInfo)
        {
            ImageMetadata = new Dictionary<string, string>();

            Id = imgurImageInfo.ID;
            IsWebBasedSource = true;
            SourceId = "ImgurViral";
            ImageUrl = imgurImageInfo.Link;
            ThumbnailUrl = imgurImageInfo.Link.Replace(imgurImageInfo.ID, imgurImageInfo.ID + "t");

            ImageMetadata.Add("height", imgurImageInfo.Height.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("id", imgurImageInfo.ID);
            ImageMetadata.Add("isAnimated", imgurImageInfo.IsAnimated.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("link", imgurImageInfo.Link);
            ImageMetadata.Add("score", imgurImageInfo.Score.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("size", imgurImageInfo.Size.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("title", imgurImageInfo.Title);
            ImageMetadata.Add("type", imgurImageInfo.Type);
            ImageMetadata.Add("ups", imgurImageInfo.Ups.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("views", imgurImageInfo.Views.ToString(CultureInfo.InvariantCulture));
            ImageMetadata.Add("width", imgurImageInfo.Width.ToString(CultureInfo.InvariantCulture));
        }
    }
}
