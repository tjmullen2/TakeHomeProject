using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TakeHomePhotoViewer.PhotoSDK;
using TakeHomePhotoViewer.PhotoSDK.Models;
using TakeHomePhotoViewer.PhotoSDK.Repositories;
using TakeHomePhotoViewer.ViewModels;
using Telerik.Windows.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace TakeHomePhotoViewer
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (PhotoCollection.GetAvailableImageRepositories().Count == 0)  // We are entering the page for the first time
            {
                // Only using CameraRoll repository for now
                PhotoCollection.RegisterRepository(new CameraRollRepository());
                PhotoCollection.RegisterRepository(new ImgurViralRepository());

                // For now, we are only concerned with the first repository (CameraRoll), ImgurViral works and can be substituted
                App.ViewModel = new PhotoCollectionViewModel(PhotoCollection.GetRepository("CameraRoll"));
                //App.ViewModel = new PhotoCollectionViewModel(PhotoCollection.GetRepository("ImgurViral"));
                
            }
            DataContext = App.ViewModel;
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
            var results = await PhotoCollection.GetImageCollection(App.ViewModel.SourceId, App.ViewModel.ImageCollection.Count, 20);
            if (results == null)
                return;
            foreach (var image in results)
            {
                App.ViewModel.ImageCollection.Add(image);
            }
        }

        // This is only necessary at the moment due to the fact that the cached images change does not trigger a change in the collection in the viewmodel
        private async void PhotoCollectionListBox_OnRefreshRequested(object sender, EventArgs e)
        {
            var results = await PhotoCollection.GetImageCollection(App.ViewModel.SourceId, App.ViewModel.ImageCollection.Count, 20);
            if (results == null)
                return;
            foreach (var image in results)
            {
                App.ViewModel.ImageCollection.Add(image);
            }
            PhotoCollectionListBox.StopPullToRefreshLoading(true);
        }

        private void PhotoCollectionListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PhotoDetailPage.xaml", UriKind.Relative));
        }
    }
}