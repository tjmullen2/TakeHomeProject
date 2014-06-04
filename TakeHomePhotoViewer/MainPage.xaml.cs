using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using TakeHomePhotoViewer.PhotoSDK;
using TakeHomePhotoViewer.PhotoSDK.Models;
using TakeHomePhotoViewer.PhotoSDK.Repositories;
using TakeHomePhotoViewer.ViewModels;

namespace TakeHomePhotoViewer
{
    public partial class MainPage : PhoneApplicationPage
    {
        private PhotoCollectionViewModel _viewModel;

        /// <summary>
        /// View model for the page to use MVVM design pattern (uses lazy loading to conserve startup time)
        /// </summary>
        public PhotoCollectionViewModel ViewModel { get { return _viewModel ?? (_viewModel = new PhotoCollectionViewModel()); } set { _viewModel = value; } }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Only using CameraRoll repository for now
            PhotoCollection.RegisterRepository(new CameraRollRepository());
            PhotoCollection.RegisterRepository(new ImgurViralRepository());

            var availableSourceIds = PhotoCollection.GetAvailableImageRepositoriesAsync();

            // For now, we are only concerned with the first repository (CameraRoll)
            // ImgurViralRepository collection works, but OutOfMemoryException is possible
            ViewModel = new PhotoCollectionViewModel(availableSourceIds[0]);

            DataContext = ViewModel;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel = null; // Detach viewmodel to conserve memory (we will reattach it when coming back)

            base.OnNavigatedFrom(e);
        }

        private void CollectionImage_OnTap(object sender, GestureEventArgs e)
        {
            if (sender == null || sender as FrameworkElement == null)
                return;                                                 // Fall out gracefully to prevent invalid casts

            var imageDetails = ((FrameworkElement) sender).DataContext as ImageSnapshotInfo;
            if (imageDetails == null)
                return;                                                 // We encountered something that could not be obtained from the datacontext, so prevent error

            string urlString = string.Format("/PhotoDetailPage.xaml?ImageId={0}&SourceId={1}",  HttpUtility.UrlEncode(imageDetails.Id), 
                                                                                                HttpUtility.UrlEncode(imageDetails.SourceId));
            NavigationService.Navigate(new Uri(urlString, UriKind.Relative));
        }

        /// <summary>
        /// Used to trigger a data request from the repository when RadDataBoundListBox needs more information
        /// Needed for data virtualization and to keep memory footprint low with many images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PhotoCollectionListBox_OnDataRequested(object sender, EventArgs e)
        {
            var results = await PhotoCollection.GetImageCollection(ViewModel.SourceId, ViewModel.ImageCollection.Count, 20);
            if (results == null)
                return;
            foreach (var image in results)
            {
                ViewModel.ImageCollection.Add(image);
            }
        }

        private async void PhotoCollectionListBox_OnRefreshRequested(object sender, EventArgs e)
        {
            var results = await PhotoCollection.GetImageCollection(ViewModel.SourceId, ViewModel.ImageCollection.Count, 20);
            if (results == null)
                return;
            foreach (var image in results)
            {
                ViewModel.ImageCollection.Add(image);
            }
            PhotoCollectionListBox.StopPullToRefreshLoading(true);
        }
    }
}