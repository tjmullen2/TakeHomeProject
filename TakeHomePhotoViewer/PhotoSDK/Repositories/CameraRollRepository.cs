using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;
using TakeHomePhotoViewer.PhotoSDK.Models;

namespace TakeHomePhotoViewer.PhotoSDK.Repositories
{
    public class CameraRollRepository : IImageRepository
    {
        public async Task<IEnumerable<ImageSnapshotInfo>> GetImagesFromRepository(int startIndex, int imageCount)
        {
            var m = new MediaLibrary();
            var images = new List<ImageSnapshotInfo>();
            int nIndex = 0;
            if (m.Pictures != null)
            {
                foreach (var r in m.Pictures)
                {
                    if (nIndex++ >= startIndex && (nIndex - 1 < (startIndex + imageCount)))
                    {
                        var snapshot = new ImageSnapshotInfo(GetRepositorySourceId(), r)
                        {
                            ThumbnailSource = await GetThumbnail(r.Name, 135, 135, true)
                        };
                        images.Add(snapshot);
                    }
                    if (nIndex - 1 == startIndex + imageCount)
                        break;
                }
            }
            return await Task.FromResult(images);
        }

        public string GetRepositorySourceId()
        {
            return "CameraRoll";
        }

        Task<IEnumerable<ImageSnapshotInfo>> IImageRepository.GetImagesFromRepositoryAsync(int startIndex, int imageCount)
        {
            return GetImagesFromRepository(startIndex, imageCount);
        }

        Task<int> IImageRepository.GetAvailableImagesFromRepositoryAsync()
        {
            return GetAvailableImagesFromRepositoryAsync();
        }

        public Task<int> GetAvailableImagesFromRepositoryAsync()
        {
            var m = new MediaLibrary();
            return Task.FromResult(m.Pictures.Count);
        }

        public Task<ImageDetailInfo> GetImageAndMetadataAsync(string imageId)
        {
            var returnValue = new ImageDetailInfo();
            var mediaLibrary = new MediaLibrary();

            foreach (var r in mediaLibrary.Pictures)
            {
                if (r.Name == imageId)
                {
                    var b = new BitmapImage();
                    b.SetSource(r.GetImage());
                    returnValue.MetadataType = "CameraRoll";
                    returnValue.ImageMetadata = new Dictionary<string, string>{     {"Created Date", r.Date.ToShortDateString()},
                                                                                    {"Image Height",r.Height.ToString(CultureInfo.InvariantCulture)},
                                                                                    {"Image Width", r.Width.ToString(CultureInfo.InvariantCulture)},
                                                                                    {"Image Name", r.Name}};
                    returnValue.LargeImage = b;
                    break;
                }
            } 
            return Task.FromResult(returnValue);
        }

        public Task<int> GetImageCountAsync()
        {
            var m = new MediaLibrary();
            return Task.FromResult(m.Pictures.Count);
        }

        Task<BitmapImage> IImageRepository.GetThumbnail(string imageId, int maxWidth, int maxHeight, bool fillMax)
        {
            return GetThumbnail(imageId, maxWidth, maxHeight, fillMax);
        }

        // Future Expansion
        public Task<string> GetThumbnailUrlAsync(string imageId, int maxWidth, int maxHeight, bool fillMax)
        {
            throw new NotImplementedException();
        }

        // Future Expansion
        public Task<string> GetImageUrlAsync(string imageId)
        {
            throw new NotImplementedException();
        }

        // Future Expansion
        public Task<bool> IsWebBasedAsync()
        {
            return Task.FromResult(false);
        }

        public Task<BitmapImage> GetThumbnail(string imageId, int maxWidth, int maxHeight, bool fillMax)
        {
            var mediaLibrary = new MediaLibrary();

            foreach (var picture in mediaLibrary.Pictures)
            {
                if (picture.Name == imageId)
                {
                    var scale = CalculateScale(picture.Width, picture.Height, maxWidth, maxHeight, fillMax);
                    var b = new BitmapImage();
                    b.SetSource(picture.GetThumbnail());
                    var wb = new WriteableBitmap(b);
                    using (var ms = new MemoryStream())
                    {
                        wb.SaveJpeg(ms, (int)(picture.Width * scale), (int)(picture.Height * scale), 0, 100);
                        b.SetSource(ms);
                        return Task.FromResult(b);
                    }
                }
            }
            return Task.FromResult(new BitmapImage());
        }

        private static float CalculateScale(int width, int height, int maxWidth, int maxHeight, bool fillMax)
        {
            float xScale = (float)maxWidth/width;
            float yScale = (float)maxHeight / height;
            return (fillMax ? Math.Min(xScale, yScale) : Math.Max(xScale, yScale));
        }
    }
}
