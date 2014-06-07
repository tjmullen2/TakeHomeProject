using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TakeHomePhotoViewer.Annotations;
using TakeHomePhotoViewer.PhotoSDK.Models;

namespace TakeHomePhotoViewer.ViewModels
{
    public class PhotoDetailViewModel : INotifyPropertyChanged
    {
        private BitmapImage _imageSource;
        private string _imageName;
        private Dictionary<string, string> _imageMetadata;
        private string _imageSourceUrl;

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

        public string ImageName
        {
            get { return _imageName; }
            set
            {
                if (value == _imageName) return;
                _imageName = value;
                OnPropertyChanged();
            }
        }

        public Dictionary<string, string> ImageMetadata { get { return _imageMetadata ?? (_imageMetadata = new Dictionary<string, string>()); } set { if (_imageMetadata == value) return;
            _imageMetadata = value;
            OnPropertyChanged();
        } }

        public string ImageSourceUrl { get { return _imageSourceUrl; } set { if (_imageSourceUrl == value) return;
            _imageSourceUrl = value;
            OnPropertyChanged();
        } }

        #region PropertyChanged Handler

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
