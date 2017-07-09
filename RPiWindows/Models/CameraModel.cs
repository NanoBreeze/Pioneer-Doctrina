using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace RPiWindows.Models
{
    class CameraModel
    {
        private static CameraModel instance = null;
        private static readonly object padlock = new object();

        private CameraModel()
        {
        }

        public static CameraModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CameraModel();
                    }
                    return instance;
                }
            }
        }

        /* The purpose for these two variables is to resolve a threading issue in optimizing displaying the images
           Because we're not awaiting when updating the image (updateImageCallback) and await on converting a stream (ConvertStreamToBufferAsync)
           we reduce the 'video latency', the lagginess. eg, 
               C C C        <= C means we call ConvertStreamToBufferAsync
                U U U       <= U means we call updateImageCallback, 
                  F F F     <= F means updateImageCallback finished executing, thus when the image is displayed on the screen. 
           Notice that we call the next C immediately after U, regardless of whether U had finished executing or not. Notice that F (when images are displayed)
           are only one space apart. However, if we did await on U, we get:
               C   C   C
                U   U   U
                  F   F   F   
           Here, notice that the space between F is larger, thus images are less responsively, so the video is more laggy. How much laggier is this? 
           Optimization reduces latency by 0.2%. At least better than nothing

            When the connection terminates (no more C, and thus, no more U and F), we want to display not the last F, but blank image (to visually indicate the connection
            has terminated). However, since we await only on C and not on U or F, we cannot simply clear the image once C stops happening, because there
            may still be Fs remaining to be finished. Thus, the F might "overwrite" our blank image. 
            How to check when the F has finished? We use counters.

            Every time C is called, increment a 'C' counter. Everytime F occurs, increment a 'F'counter. When connection has terminated (C no longer being called), 
            not all Fs have occured, so we wait until the number of 'F' counters matches the number of 'C' counters. When that happens, show the blank screen.
            The 'C' counter is ProcessStreamToImageCounter and 'F' counter is 'DisplayImageCounter'
          */

        public int ConvertStreamToBufferCounter { get; set; }
        public int ImageDisplayedFromBufferCounter { get; set; }
    }
}
