using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using RPiWindows.Models;

namespace RPiWindows.Servers
{
    class CameraServer
    {
        private static int i = 0;
        private const int ONE_MEGABYTE = 1000000;
        private byte[] lengthBuffer; // For optimization to avoid creating one every time
        private IBuffer imageBuffer; // For optimization to avoid creating one every time
        private Action<IBuffer> updateImageCallback;

        public CameraServer(Action<IBuffer> updateImageCallback)
        {
            this.updateImageCallback = updateImageCallback;
            lengthBuffer = new byte[4];
            imageBuffer = new Windows.Storage.Streams.Buffer(ONE_MEGABYTE); // Don't expect to ever be 1MB but this is the upper limit
        }


        public async Task HandleIncomingStreamAsync(IInputStream inputStream)
        {
            while (true)
            {
                IBuffer imageBuffer = await ConvertStreamToBufferAsync(inputStream);

                if (imageBuffer == null || imageBuffer.Length == 0)
                {
                    return;
                }
                CameraModel.Instance.ConvertStreamToBufferCounter++;

                updateImageCallback(imageBuffer); // I think adding an await here would cause updating the image to be slower (since we have to wait for display before reading stream again)
                // Check out CameraModel.cs for comments about resolving potential threading issue that this introduced
            }
        }

        private async Task<IBuffer> ConvertStreamToBufferAsync(IInputStream inputStream)
        {
            // Don't expect to ever be 1MB but this is the upper limit Block until we get all the data
            await
                inputStream.ReadAsync(lengthBuffer.AsBuffer(), lengthBuffer.AsBuffer().Capacity, InputStreamOptions.None);

            // We don't expect the image size to fill all of UInt32, ONE_MEGABYTE should be the image size's upper limit 
            // We don't use BitConverter because we can't specify whether to convert with little or big endian. 
            // The camera client uses big endian so here, we will use bit operations to get the data
            UInt32 imageLength = Convert.ToUInt32(
                (lengthBuffer[0] << 24 | lengthBuffer[1] << 16 | lengthBuffer[2] << 8 | lengthBuffer[3]));

            Debug.WriteLine("The value of imageLength is: " + Convert.ToString(imageLength));
            if (imageLength > 0)
            {
                await inputStream.ReadAsync(imageBuffer, imageLength, InputStreamOptions.None);
                return imageBuffer;

                //    StorageFolder folder = KnownFolders.PicturesLibrary;
                //    if (folder != null)
                //    {
                //        StorageFile file =
                //            await
                //                folder.CreateFileAsync("newImage" + Convert.ToString(i) + ".jpg",
                //                    CreationCollisionOption.GenerateUniqueName);
                //        await FileIO.WriteBufferAsync(file, result);
                //    }
                //}
            }
            return null;
        }

        public async Task SaveImageAsync(string imageName, StorageFolder folder, IBuffer imageBuffer)
        {
            if (imageName != null && folder != null && imageBuffer.Length > 0)
            {
                StorageFile file = await
                        folder.CreateFileAsync(imageName, CreationCollisionOption.GenerateUniqueName);
                await FileIO.WriteBufferAsync(file, imageBuffer);
            }
        }
    }
}
