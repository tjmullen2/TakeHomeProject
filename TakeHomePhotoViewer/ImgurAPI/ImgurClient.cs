using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TakeHomePhotoViewer.ImgurAPI.Models;

namespace TakeHomePhotoViewer.ImgurAPI
{
    public class ImgurClient
    {
        private string _clientID;
        private string _clientSecret;

        public ImgurClient(string clientID, string clientSecret)
        {
            _clientID = clientID;
            _clientSecret = clientSecret;
        }

        /// <summary>
        /// Get the images from the main gallery.
        /// This call DOES NOT require authentcation.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        public async Task<ImgurImageData> GetMainGalleryImages(ImgurGallerySection section, ImgurGallerySort sort, int page)
        {
            string _sort = sort.ToString().ToLower();
            string _section = section.ToString().ToLower();

            var client = new WebClient();
            client.Headers["Authorization"] = "Client-ID " + _clientID;

            var s = await client.DownloadStringTask(new Uri(string.Format(ImgurEndpoints.MainGallery, _section, _sort, page)));
            return JsonConvert.DeserializeObject<ImgurImageData>(s);
        }

        public async Task<ImgurSingleImage> GetImageDetails(string imageId)
        {
            var client = new WebClient();
            client.Headers["Authorization"] = "Client-ID " + _clientID;

            var s = await client.DownloadStringTask(new Uri(string.Format(ImgurEndpoints.SingleImage, imageId)));
            return JsonConvert.DeserializeObject<ImgurSingleImage>(s);
        }
    }

    // Added asynchronous extension to avoid refreshing data source
    public static class Extensions
    {
        public static Task<string> DownloadStringTask(this WebClient webClient, Uri uri)
        {
            var tcs = new TaskCompletionSource<string>();

            webClient.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.SetException(e.Error);
                }
                else
                {
                    tcs.SetResult(e.Result);
                }
            };

            webClient.DownloadStringAsync(uri);

            return tcs.Task;
        }
    }
}
