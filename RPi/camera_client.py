import io
import socket
import struct
import time
import picamera

client_socket = socket.socket()
client_socket.connect(('127.0.0.1', 8000))

connection = client_socket.makefile('wb')
try:
    camera = picamera.PiCamera()
    camera.resolution = (640, 480)
    # Start a preview and let the camera warm up for 1 seconds
    camera.start_preview()
    time.sleep(1)

    start = time.time()
    stream = io.BytesIO()
    for _ in camera.capture_continuous(stream, 'jpeg'):
        # Write the length of the capture to the stream and flush to ensure it actually gets sent
        connection.write(struct.pack('<L', stream.tell()))
        connection.flush()
        # Rewind the stream and send the image data over the wire
        stream.seek(0)
        connection.write(stream.read())

        if time.time() - start > 30:
            break
        # Reset the stream for the next capture
        stream.seek(0)
        stream.truncate()
    # Write a length of zero to the stream to signal we're done
    connection.write(struct.pack('<L', 0))
finally:
    connection.close()
    client_socket.close()
