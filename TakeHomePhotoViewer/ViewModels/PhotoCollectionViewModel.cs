using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TakeHomePhotoViewer.Annotations;
using TakeHomePhotoViewer.PhotoSDK;
using TakeHomePhotoViewer.PhotoSDK.Models;

namespace TakeHomePhotoViewer.ViewModels
{
    public class PhotoCollectionViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The source id (key) for this collection 
        /// </summary>
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

        private string _sourceId;

        public ObservableCollection<ImageSnapshotInfo> ImageCollection
        {
            get { return _imageCollection ?? (_imageCollection = new ObservableCollection<ImageSnapshotInfo>()); }
            set { _imageCollection = value; OnPropertyChanged(); }
        }
        
        public PhotoCollectionViewModel()
        {
        }

        public PhotoCollectionViewModel(string sourceId)
        {
            SourceId = sourceId;
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
