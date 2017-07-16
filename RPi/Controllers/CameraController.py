import struct

class CameraController:

	END_OF_STREAM = 0

	def __init__(self, cameraClient):
		self.cameraClient = cameraClient
		print("Camera controller here")

	def onImageSizeFound(self, size):
		''' Send big endian of image size '''
		print("Inside onImageSizeFound")
		packed_size = struct.pack('>L', size) # Use big endian
		self.cameraClient.send(packed_size)

	def onImageStreamRead(self, imageStream):
		print("Inside onImageStreamRead")
		self.cameraClient.send(imageStream)

	def onTerminateCamera(self):
		self.cameraClient.send(self.END_OF_STREAM)
		self.cameraClient.closeConnection()




