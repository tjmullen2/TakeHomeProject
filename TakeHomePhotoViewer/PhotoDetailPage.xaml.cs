using System.Net;
using System.Windows.Navigation;
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
    }
}