import io
import socket
import struct
import time
import picamera
from PIL import Image



client_socket = socket.socket()
client_socket.connect(('10.0.0.96', 8000))

connection = client_socket.makefile('wb')
try:
    camera = picamera.PiCamera()
    camera.resolution = (640, 480)
    # Start a preview and let the camera warm up for 1 seconds
    # camera.start_preview()
    time.sleep(1)


    i = 0
    stream = io.BytesIO()
    for _ in camera.capture_continuous(stream, 'jpeg'):
        # Write the length of the capture to the stream and flush to ensure it actually gets sent
        
	
 	print("About to call stream.seek(0)")
	stream.seek(0)
	image = Image.open(stream)
	print("About to save image")

	image.save('imageHere.jpeg')
	print("image saved")
	


	tell = stream.tell()
	packed_size = struct.pack('>L', tell)
	print(str(tell))
        connection.write(packed_size) # Use big endian
	#tell = stream.tell()
        #print(str(tell))
        #connection.write(tell)
        connection.flush()
	
        # Rewind the stream and send the image data over the wire
        stream.seek(0)
        connection.write(stream.read())
        connection.flush()

        if i == 5:
            break

	i = i + 1
        # Reset the stream for the next capture
	
        stream.seek(0)
        stream.truncate()
    # Write a length of zero to the stream to signal we're done

    END_OF_STREAM = 3 # Same as on server 
    connection.write(struct.pack('>L', 0))
    camera.close()

finally:
    connection.close()
    client_socket.close()
	
