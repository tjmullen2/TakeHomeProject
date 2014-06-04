using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TakeHomePhotoViewer.ImgurAPI.Models
{
    public class ImgurImageData
    {
        [JsonProperty(PropertyName = "data")]
        public IEnumerable<ImgurImage> Images { get; set; }
    }
}
