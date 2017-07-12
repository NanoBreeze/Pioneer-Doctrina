import socket

class UdpServer:
   
	def __init__(self, clientIpAddress, port,  movementController):
		self.serverSocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
		print("About to bind to" + clientIpAddress + ":" + str(port))
		self.serverSocket.bind((clientIpAddress, port))
		self.movementController = movementController
		self.waitForClient()

	def waitForClient(self):
		while True:
			data = self.serverSocket.recv(1024)
			data = data.decode(encoding='utf-8').strip() # utf-8 is Python's default. We want this to match our list of allowed data. Also remove the \r\n at the end of the data
			print("Received data!" + data)
			self.movementController.onReceiveMovement(data)
		self.closeSocket()
   
	def closeSocket(self):
		self.serverSocket.close()
