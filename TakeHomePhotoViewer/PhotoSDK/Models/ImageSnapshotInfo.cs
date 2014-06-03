using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;
using TakeHomePhotoViewer.Annotations;

namespace TakeHomePhotoViewer.PhotoSDK.Models
{
    public class ImageSnapshotInfo : INotifyPropertyChanged
    {
        private BitmapImage _thumbnailSource;
        private BitmapImage _imageSource;
        private string _id;
        private string _thumbnailUrl;
        private string _imageUrl;
        private bool _isWebBasedSource;
        private string _sourceId;

        public ImageSnapshotInfo(string sourceId, Picture picture)
        {
            this.SourceId = sourceId;
            this.Id = picture.Name;
        }

        public ImageSnapshotInfo()
        {
            
        }

        public string Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        // For local sources
        public BitmapImage ThumbnailSource
        {
            get { return _thumbnailSource; }
            set
            {
                if (Equals(value, _thumbnailSource)) return;
                _thumbnailSource = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (Equals(value, _imageSource)) return;
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        // For online sources
        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set
            {
                if (value == _thumbnailUrl) return;
                _thumbnailUrl = value;
                OnPropertyChanged();
            }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (value == _imageUrl) return;
                _imageUrl = value;
                OnPropertyChanged();
            }
        }

        public bool IsWebBasedSource
        {
            get { return _isWebBasedSource; }
            set
            {
                if (value.Equals(_isWebBasedSource)) return;
                _isWebBasedSource = value;
                OnPropertyChanged();
            }
        }

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


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}