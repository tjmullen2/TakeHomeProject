using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeHomePhotoViewer.PhotoSDK.Models;
using TakeHomePhotoViewer.PhotoSDK.Repositories;
using TakeHomePhotoViewer.ViewModels;

namespace TakeHomePhotoViewer.PhotoSDK
{
    public static class PhotoCollection
    {
        #region Private Data Structure
        private static readonly List<IImageRepository> Repositories = new List<IImageRepository>();
        #endregion

        /// <summary>
        /// Obtains a list of keys that are available
        /// </summary>
        /// <returns>List of strings</returns>
        public static List<string> GetAvailableImageRepositories()
        {
            var returnVal = Repositories.Select(repository => repository.GetRepositorySourceId()).ToList();

            // Add appropriate repositories

            return returnVal;
        }

        /// <summary>
        /// Registers a new repository for use
        /// </summary>
        /// <param name="repository">Repository to register</param>
        public static void RegisterRepository(IImageRepository repository)
        {
            Repositories.Add(repository);
        }

        /// <summary>
        /// Asynchronously obtains an enumeration of images within the specified collection
        /// </summary>
        /// <param name="sourceId">Image repository key</param>
        /// <param name="startIndex">Start of the requested images in the collection</param>
        /// <param name="imageCount">Number of requested images in the collection</param>
        /// <returns>IEnumerable of ImageSnapshotInfo</returns>
        public static Task<IEnumerable<ImageSnapshotInfo>> GetImageCollection(string sourceId, int startIndex, int imageCount)
        {
            return GetRepository(sourceId).GetImagesFromRepositoryAsync(startIndex, imageCount);
        }

        /// <summary>
        /// This is to get the repository by a specific Id (maybe to have different tabs and the viewmodel for each tab keep track of the repository
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public static IImageRepository GetRepository(string sourceId)
        {
            return Repositories.FirstOrDefault(repository => repository.GetRepositorySourceId() == sourceId);
        }

        /// <summary>
        /// Future use: Particularly if we have web services that serve as repositories, this would be helpful to check for empty
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns>int</returns>
        public static Task<int> GetAvailableImageCount(string sourceId)
        {
            return GetRepository(sourceId).GetAvailableImagesFromRepositoryAsync();
        }
    }
}
