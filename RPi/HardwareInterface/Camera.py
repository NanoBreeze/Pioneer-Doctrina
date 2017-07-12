import io
import picamera
import time
from PIL import Image


class Camera:

	def __init__(self, cameraController):
		self.cameraController = cameraController
		print("Camera here")

	def captureContinuousStream(self):
		try:
	# Let camera warm up for 1 second
			self.camera = picamera.PiCamera()
			self.camera.resolution = (640, 480)
			time.sleep(1)

			stream = io.BytesIO()
			for _ in self.camera.capture_continuous(stream, 'jpeg'):
			# Write the length of the capture to the stream and flush to ensure it actually gets sent

				tell = stream.tell()
				print(str(tell))
				# packed_size = struct.pack('>L', tell) # Use big endian
				self.cameraController.onImageSizeFound(tell)

				stream.seek(0) # Rewind the stream and send the image data over the wire
				self.cameraController.onImageStreamRead(stream.read())

				# Reset the stream for the next capture
				stream.seek(0)
				stream.truncate()

			# Write a length of zero to the stream to signal we're done
			self.cameraController.onTerminateCamera() 
		finally:
			self.cameraController.onTerminateCamera()
			self.camera.close()

