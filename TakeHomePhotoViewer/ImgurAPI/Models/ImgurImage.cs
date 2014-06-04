using System;
using Newtonsoft.Json;

namespace TakeHomePhotoViewer.ImgurAPI.Models
{
    public class ImgurImage
    {

        public string ID { get; set; }

        public string Title { get; set; }

        public Int64 DateTime { get; set; }

        public string Type { get; set; }

        [JsonProperty(PropertyName = "animated")]

        public bool IsAnimated { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Int64 Size { get; set; }

        public Int64 Views { get; set; }

        [JsonProperty(PropertyName = "account_url")]

        public string AccountUrl { get; set; }

        public string Link { get; set; }

        public string Bandwidth { get; set; }

        public int Ups { get; set; }

        public int Downs { get; set; }

        public int Score { get; set; }

        [JsonProperty(PropertyName = "is_album")]

        public bool IsAlbum { get; set; }

    }
}