using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TakeHomePhotoViewer.PhotoSDK;
using TakeHomePhotoViewer.PhotoSDK.Repositories;
using TakeHomePhotoViewer.ViewModels;

namespace TakeHomePhotoViewer
{
    public partial class PhotoDetailPage : PhoneApplicationPage
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