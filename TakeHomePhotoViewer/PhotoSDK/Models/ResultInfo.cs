namespace TakeHomePhotoViewer.PhotoSDK.Models
{
    /// <summary>
    /// Class for future use when obtaining results from a web service
    /// </summary>
    public class ResultInfo
    {
        public long TotalResults { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public long StartIndex { get; set; }
        public long RequestCount { get; set; }
    }
}