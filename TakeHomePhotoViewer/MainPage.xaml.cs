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
        /// <summary>
        /// View model for the page
        /// </summary>
        private PhotoCollectionViewModel _viewModel;
        public PhotoCollectionViewModel ViewModel { get { return _viewModel ?? (_viewModel = new PhotoCollectionViewModel()); } set { _viewModel = value; } }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Only using CameraRoll repository for now
            PhotoCollection.RegisterRepository(new CameraRollRepository());

            var availableSourceIds = await PhotoCollection.GetAvailableImageRepositoriesAsync();
            ViewModel = new PhotoCollectionViewModel(availableSourceIds[0]);

            DataContext = ViewModel;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel = null; // Detach viewmodel to conserve memory (we will reattach it when coming back)

            base.OnNavigatedFrom(e);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        private void CollectionImage_OnTap(object sender, GestureEventArgs e)
        {
            var imageDetails = ((FrameworkElement) sender).DataContext as ImageSnapshotInfo;
            if (imageDetails == null)
                return; // We encountered something that could not be obtained from the datacontext, so prevent error

            string urlString = string.Format("/PhotoDetailPage.xaml?ImageId={0}&SourceId={1}", HttpUtility.UrlEncode(imageDetails.Id), HttpUtility.UrlEncode(imageDetails.SourceId));
            NavigationService.Navigate(new Uri(urlString, UriKind.Relative));
        }

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
    }
}