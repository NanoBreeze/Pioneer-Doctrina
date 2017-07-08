using System;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace RPiWindows.Servers
{
    class CameraServer
    {
        private static int i = 0;
        private const int ONE_MEGABYTE = 1000000;
        public async Task HandleInputStream(IInputStream inputStream)
        {
            byte[] lengthBuffer = new byte[4];
            byte[] imageBuffer = new byte[ONE_MEGABYTE]; // Don't expect to ever be 1MB but this is the upper limit

            while (true)
            {
                i++;
                // Block until we get all the data
                await inputStream.ReadAsync(lengthBuffer.AsBuffer(), lengthBuffer.AsBuffer().Capacity, InputStreamOptions.None);

                // We don't expect the image size to fill all of UInt32, ONE_MEGABYTE should be the image size's upper limit 
                // We don't use BitConverter because we can't specify whether to convert with little or big endian. 
                // The camera client uses big endian so here, we will use bit operations to get the data
                UInt32 imageLength = Convert.ToUInt32(
                    (lengthBuffer[0] << 24 | lengthBuffer[1] << 16 | lengthBuffer[2] << 8 | lengthBuffer[3]));

                Debug.WriteLine("The value of imageLength is: " + Convert.ToString(imageLength));
                if (imageLength > 0)
                {
                    IBuffer result =
                        await inputStream.ReadAsync(imageBuffer.AsBuffer(), imageLength, InputStreamOptions.None);

                    StorageFolder folder = KnownFolders.PicturesLibrary;
                    if (folder != null)
                    {
                        StorageFile file =
                            await
                                folder.CreateFileAsync("newImage" + Convert.ToString(i) + ".jpg",
                                    CreationCollisionOption.GenerateUniqueName);
                        await FileIO.WriteBufferAsync(file, result);
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}
