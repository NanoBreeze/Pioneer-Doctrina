import socket

class CameraClient:
   
	def __init__(self, ipAddress, port):
		''' ip_address is a string and port is an int '''
		self.clientSocket = socket.socket()
		self.clientSocket.connect((ipAddress, port))
		self.connection = self.clientSocket.makefile('wb')
		print("Camera client here")

	def send(self, data):
		self.connection.write(data) 
		self.connection.flush()

	def closeConnection(self):
		self.connection.close()
		self.clientSocket.close();

