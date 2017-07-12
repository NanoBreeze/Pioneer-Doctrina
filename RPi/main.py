import sys
sys.path.insert(0,'/home/pi/Desktop/Pioneer-Doctrina/RPi/LANConnections')  # A bit hacky to import from another directory
sys.path.insert(0,'/home/pi/Desktop/Pioneer-Doctrina/RPi/Controllers')  
sys.path.insert(0,'/home/pi/Desktop/Pioneer-Doctrina/RPi/HardwareInterface')  

from threading import Thread
from MovementController import MovementController
from UdpServer import UdpServer
from BluetoothServer import BluetoothServer
from Gpio import Gpio

from CameraClient import CameraClient
from Camera import Camera
from CameraController import CameraController


SELF_IP_ADDRESS = '10.0.0.109'
SELF_PORT = 1337

SELF_BLUETOOTH_MAC = 'B8:27:EB:9C:80:A6' # or is it A8?
SELF_CHANNEL = 1

SERVER_CAMERA_IP_ADDRESS = '10.0.0.96'
SERVER_CAMERA_PORT = 8000


def startUdpServer(bindIpAddress, bindPort, movementController):
	udpServer = UdpServer(bindIpAddress, bindPort, movementController)


def startBluetoothServer(bindMac, bindChannel, movementController):
	bluetoothServer = BluetoothServer(bindMac, bindChannel, movementController)


def startCamera(serverIpAddress, serverPort):
	cameraClient = CameraClient(serverIpAddress, serverPort)
	cameraController = CameraController(cameraClient)
	camera = Camera(cameraController)
	camera.captureContinuousStream()

if __name__ == '__main__':
	print("About to call Gpio()")
	gpio = Gpio()
	movementController = MovementController(gpio)

	udpServerThread = Thread(target=startUdpServer, args=(SELF_IP_ADDRESS, SELF_PORT, movementController,))
	udpServerThread.start()

	btServerThread = Thread(target=startBluetoothServer, args=(SELF_BLUETOOTH_MAC, SELF_CHANNEL, movementController,))
	btServerThread.start()


	cameraClientThread = Thread(target=startCamera, args=(SERVER_CAMERA_IP_ADDRESS, SERVER_CAMERA_PORT))
	cameraClientThread.start()

	print("hello")

