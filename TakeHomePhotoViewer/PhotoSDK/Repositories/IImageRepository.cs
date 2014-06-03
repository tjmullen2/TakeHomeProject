using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TakeHomePhotoViewer.PhotoSDK.Models;
using TakeHomePhotoViewer.ViewModels;

namespace TakeHomePhotoViewer.PhotoSDK.Repositories
{
    public interface IImageRepository
    {
        /// <summary>
        /// Gets the repositoryId
        /// </summary>
        /// <returns>string</returns>
        string GetRepositorySourceId();

        /// <summary>
        /// Asynchronously obtains an IEnumerable of ImageSnapshotInfo in the range specified from the collection
        /// </summary>
        /// <param name="startIndex">Start of the requested images in the collection</param>
        /// <param name="imageCount">Number of requested images in the collection</param>
        /// <returns>IEnumerable</returns>
        Task<IEnumerable<ImageSnapshotInfo>> GetImagesFromRepositoryAsync(int startIndex, int imageCount);

        /// <summary>
        /// Asynchronously returns the available images in the collection
        /// </summary>
        /// <returns>int</returns>
        Task<int> GetAvailableImagesFromRepositoryAsync();

        /// <summary>
        /// Asynchronously obtains the original image and metadata for a single image
        /// </summary>
        /// <param name="imageId">Unique ID to identify the image in the collection</param>
        /// <returns>ImageDetailInfo</returns>
        Task<ImageDetailInfo> GetImageAndMetadataAsync(string imageId);
        
        /// <summary>
        /// Asynchronously obtains the count of images in the collection
        /// </summary>
        /// <returns>Integer indicating available images</returns>
        Task<int> GetImageCountAsync();

        /// <summary>
        /// Asynchronously obtains the thumbnail as a bitmap associated with the image in the collection
        /// </summary>
        /// <param name="imageId">Unique ID to identify the image in the collection</param>
        /// <param name="maxWidth">Maximum requested width of the image</param>
        /// <param name="maxHeight">Maximum requested height of the image</param>
        /// <param name="fillMax">Whether the image will fill the thumbnail or not</param>
        /// <returns>String indicating the url of the closest matching image available</returns>
        Task<BitmapImage> GetThumbnail(string imageId, int maxWidth, int maxHeight, bool fillMax);

        /// <summary>
        /// Asynchronously obtains the thumbnail as a bitmap associated with the image in the collection
        /// </summary>
        /// <param name="imageId">Unique ID to identify the image in the collection</param>
        /// <param name="maxWidth">Maximum requested width of the image</param>
        /// <param name="maxHeight">Maximum requested height of the image</param>
        /// <param name="fillMax">Whether the image will fill the thumbnail or not</param>
        /// <returns>String indicating the url of the closest matching image available</returns>
        Task<string> GetThumbnailUrlAsync(string imageId, int maxWidth, int maxHeight, bool fillMax);

        /// <summary>
        /// Asynchronous call used to get the url of the original image
        /// </summary>
        /// <param name="imageId">Unique ID to identify the image in the collection</param>
        /// <returns>string</returns>
        Task<string> GetImageUrlAsync(string imageId);

        /// <summary>
        /// Asynchronous call used to indicate whether or not the image is web based. 
        /// </summary>
        /// <returns>bool</returns>
        /// <remarks>Is a method rather than a property as it should be implemented in the interface.</remarks>
        Task<bool> IsWebBasedAsync();
    }
}
