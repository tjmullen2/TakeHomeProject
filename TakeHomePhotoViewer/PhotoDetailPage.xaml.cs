using System;
using System.Net;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;
using TakeHomePhotoViewer.PhotoSDK;
using TakeHomePhotoViewer.ViewModels;

namespace TakeHomePhotoViewer
{
    public partial class PhotoDetailPage
    {
        public PhotoDetailPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        private void Image_OnDoubleTap(object sender, GestureEventArgs e)
        {
            // If we want to expand this by implementing double-tap on the image, 
            // we could open a browser to the link (would require testing for link available and valid)

            // var webBrowserTask = new WebBrowserTask{ Uri = new Uri(App.ViewModel.SelectedImage.ImageMetadata["link"])}
        }
    }
}