using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TakeHomePhotoViewer.Annotations;
using TakeHomePhotoViewer.PhotoSDK;
using TakeHomePhotoViewer.PhotoSDK.Models;
using TakeHomePhotoViewer.PhotoSDK.Repositories;

namespace TakeHomePhotoViewer.ViewModels
{
    public class PhotoCollectionViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The source id (key) for this collection 
        /// </summary>
        private string _sourceId;
        public string SourceId
        {
            get { return _sourceId; }
            set
            {
                if (value == _sourceId) return;
                _sourceId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Container for images
        /// </summary>
        private ObservableCollection<ImageSnapshotInfo> _imageCollection;
        public ObservableCollection<ImageSnapshotInfo> ImageCollection
        {
            get { return _imageCollection ?? (_imageCollection = new ObservableCollection<ImageSnapshotInfo>()); }
            set { _imageCollection = value; OnPropertyChanged(); }
        }

        private ImageSnapshotInfo _selectedImage;
        public ImageSnapshotInfo SelectedImage { get { return _selectedImage; } set { _selectedImage = value; OnPropertyChanged(); } }

        #region Contructors

        public PhotoCollectionViewModel()
        {
        }

        public PhotoCollectionViewModel(string sourceId)
        {
            SourceId = sourceId;
        }

        public PhotoCollectionViewModel(IImageRepository repository)
        {
            SourceId = repository.GetRepositorySourceId();
            ImageCollection = new ObservableCollection<ImageSnapshotInfo>();
            // Attach RepositoryCollectionChanged handler
            repository.RepositoryCollectionChanged += repository_RepositoryCollectionChanged;
        }

        #endregion

        async void repository_RepositoryCollectionChanged(object sender, System.EventArgs e)
        {
            var newCollection = await PhotoCollection.GetRepository(SourceId).GetImagesFromRepositoryAsync(0, 20);
            ImageCollection.Clear();
            foreach (var image in newCollection)
            {
                ImageCollection.Add(image);
            }
        }

        #region PropertyChanged Support

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
