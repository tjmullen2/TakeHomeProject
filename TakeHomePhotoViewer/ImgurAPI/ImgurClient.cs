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
        public void GetMainGalleryImages(ImgurGallerySection section, ImgurGallerySort sort, int page,
                                         Action<ImgurImageData> onCompletion)
        {
            string _sort = sort.ToString().ToLower();
            string _section = section.ToString().ToLower();

            WebClient client = new WebClient();
            client.Headers["Authorization"] = "Client-ID " + _clientID;

            client.DownloadStringAsync(new Uri(string.Format(ImgurEndpoints.MainGallery, _section, _sort, page)));
            client.DownloadStringCompleted += (c, s) =>
                {
                    var imageData = JsonConvert.DeserializeObject<ImgurImageData>(s.Result);
                    onCompletion(imageData);
                };
        }
    }
}
