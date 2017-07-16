using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using RPiWindows.Models;

namespace RPiWindows.Servers
{
    class CameraServer
    {
        private static byte[] lengthBuffer; // For optimization to avoid creating one every time
        private byte[] imageBytes; // For optimization to avoid creating one every time
        private Action<byte[]> updateImageCallback;

        public CameraServer(Action<byte[]> updateImageCallback)
        {
            this.updateImageCallback = updateImageCallback;
            lengthBuffer = new byte[4];
        }


        public async Task HandleIncomingStreamAsync(IInputStream inputStream)
        {
            while (true)
            {
                imageBytes = await ConvertStreamToByteArrayAsync(inputStream);

                if (imageBytes == null)
                {
                    return;
                }

                CameraModel.Instance.ImageBytes = (byte[]) imageBytes.Clone();
                CameraModel.Instance.ConvertStreamToBufferCounter++;
                updateImageCallback((byte[])imageBytes.Clone()); // we use clone to avoid using same address, lest it be overriden by next round before the bytes are used
                // Adding await here along with extension method to await Run(Task)Async would cause updating the image to be slower (since we have to wait for display before reading stream again)
                // Check out CameraModel.cs for comments about resolving potential threading issue that this introduced
            }
        }

        private async Task<byte[]> ConvertStreamToByteArrayAsync(IInputStream inputStream)
        {
            // Don't expect to ever be 1MB but this is the upper limit Block until we get all the data
            await
                inputStream.ReadAsync(lengthBuffer.AsBuffer(), lengthBuffer.AsBuffer().Capacity, InputStreamOptions.None);

            // We don't expect the image size to fill all of UInt32, ONE_MEGABYTE should be the image size's upper limit 
            // We don't use BitConverter because we can't specify whether to convert with little or big endian. 
            // The camera client uses big endian so here, we will use bit operations to get the data
            UInt32 imageLength = Convert.ToUInt32(
                (lengthBuffer[0] << 24 | lengthBuffer[1] << 16 | lengthBuffer[2] << 8 | lengthBuffer[3]));
            imageBytes = new byte[imageLength];
            if (imageLength > 0 && imageLength < 1000000) // If client suddenly stops, the data it sends is corrupted. Crude way of checking for corrpution
            {
                await inputStream.ReadAsync(imageBytes.AsBuffer(), imageLength, InputStreamOptions.None);
                return imageBytes;
            }
            return null;
        }

        public async Task SaveImageAsync(string imageName, StorageFolder folder, byte[] imageBytes)
        {
            if (imageName != null && folder != null && imageBytes.Length > 0)
            {
                StorageFile file = await
                        folder.CreateFileAsync(imageName, CreationCollisionOption.GenerateUniqueName);
                await FileIO.WriteBytesAsync(file, imageBytes);
            }
        }
    }
}
