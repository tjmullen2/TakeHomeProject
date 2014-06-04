using System.Net;
using System.Windows.Navigation;
using TakeHomePhotoViewer.PhotoSDK;
using TakeHomePhotoViewer.ViewModels;

namespace TakeHomePhotoViewer
{
    public partial class PhotoDetailPage
    {
        private PhotoDetailViewModel _viewModel;
        public PhotoDetailViewModel ViewModel { get { return _viewModel ?? (_viewModel = new PhotoDetailViewModel()); } }

        public PhotoDetailPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // TODO: Test for querystring keys before access to prevent possible error (should never occur, but better safe than sorry
            var imageId = HttpUtility.UrlDecode(NavigationContext.QueryString["ImageId"]);
            var sourceId = HttpUtility.UrlDecode(NavigationContext.QueryString["SourceId"]);

            if (string.IsNullOrEmpty(imageId) || string.IsNullOrEmpty(sourceId))
                return;

            var results = await PhotoCollection.GetImageDetail(sourceId, imageId);
            ViewModel.LoadResults(results);
            DataContext = ViewModel;
        }
    }
}