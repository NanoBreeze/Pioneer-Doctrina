using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Streams;

namespace RPiWindows.Servers
{
    class CameraServer
    {
        public async void HandleInputStream(IInputStream inputStream)
        {
            byte[] length = new byte[6];
            byte[] inbuffer = new byte[10000000];

            // Block until we get all the data
            IBuffer len = await inputStream.ReadAsync(length.AsBuffer(), length.AsBuffer().Capacity, InputStreamOptions.None);
            IBuffer result = await inputStream.ReadAsync(inbuffer.AsBuffer(), inbuffer.AsBuffer().Capacity, InputStreamOptions.None);

            StorageFolder folder = KnownFolders.PicturesLibrary;
            if (folder != null)
            {
                StorageFile file = await folder.CreateFileAsync("newImage" + ".jpg", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBufferAsync(file, result);
            }
        }
    }
}
