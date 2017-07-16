import bluetooth # PyBluez

class BluetoothServer:

	def __init__(self, clientMAC, channel,  movementController):
		self.serverSocket = bluetooth.BluetoothSocket(bluetooth.RFCOMM)
		self.serverSocket.bind((clientMAC, channel))
		self.movementController = movementController
		self.listenAndWaitForClient()

	def listenAndWaitForClient(self):
		self.serverSocket.listen(1)
		clientSocket, clientAddress = self.serverSocket.accept()
		print("accepted connection from " + str(clientAddress))

		while True:
			data = clientSocket.recv(1024)
			data = data.decode('utf-8').strip()
			self.movementController.onReceiveMoment(data)

	def closeSocket():
		self.serverSocket.close()
