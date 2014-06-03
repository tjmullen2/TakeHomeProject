using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using TakeHomePhotoViewer.PhotoSDK.Models;
using TakeHomePhotoViewer.PhotoSDK.Repositories;

namespace TakeHomePhotoViewer.ViewModels
{
    public class ImageSnapshotCollection : IList<ImageSnapshotInfo>, IList
    {
        public IImageRepository RepositorySource { get; set; }

        public IEnumerator<ImageSnapshotInfo> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ImageSnapshotInfo item)
        {
            throw new NotImplementedException();
        }

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(ImageSnapshotInfo item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ImageSnapshotInfo[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ImageSnapshotInfo item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count { get; set; }
        public bool IsSynchronized { get; private set; }
        public object SyncRoot { get; private set; }
        public bool IsReadOnly { get; private set; }

        object IList.this[int index]
        {
            get
            {
                // Obtain the information we need from the collection
                Debug.WriteLine("Requested item " + index.ToString(CultureInfo.InvariantCulture));
                var result = RepositorySource.GetImagesFromRepositoryAsync(index, 1).Result;
                return result.GetEnumerator().Current;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int IndexOf(ImageSnapshotInfo item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ImageSnapshotInfo item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize { get; private set; }

        public ImageSnapshotInfo this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}