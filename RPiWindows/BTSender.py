import socket
import sys
import bluetooth
import signal

'''
It is not possible for UWP to communicate via Bluetooth to the Raspberry Pi. I suspect
this is due to system or protocol incompatibility (many other people have the same errors
as me and virtually no replies to their questions). As a result, this Python program
acts as the Bluetooth client. 
Since UWP forbids IPC pipes, we get data from the UWP program via localhost. The UWP sends 
data to a port that we listen on. Upon receiving data on said port, transmit via Bluetooth
'''


# Bluetooth address
BD_ADDR = "B8:27:EB:9C:80:A8" # Raspberry Pi
CHANNEL = 1

# localhost info
HOST = 'localhost'
PORT = 10001

bt_sock = None
tcp_sock = None

def sigint_handler(signal, frame):
	tcp_sock.close()
	bt_sock.close()	


if __name__ == "__main__":

	# Set up SIGINT handler (eg, if user presses Ctrl-C, we can gracefully terminate program)
	signal.signal(signal.SIGINT, sigint_handler)

	# Connect with bluetooth
	bt_sock=bluetooth.BluetoothSocket( bluetooth.RFCOMM )
	try:
		bt_sock.connect((BD_ADDR, CHANNEL))
	except bluetooth.btcommon.BluetoothError as err:
		print("Bluetooth connection error. Error message: " + err)
		sys.exit()

	# Create socket
	try:
		tcp_sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
	except socket.error as msg:
		print("Error on creating socket. Error Code: " + str(msg[0]) +
			" Message: " + msg[1])
		sys.exit()

	# Bind and listen
	try:
		tcp_sock.bind((HOST, PORT))
	except socket.error as msg:
		print("Error occured on bind. Error code:" + str(msg[0]) +
	 		" Message: " + msg[1])
		sys.exit()

	while True:
		data, address = tcp_sock.recvfrom(4096)
		if data:
			print("Data: " + data.decode() + " | Address: " + str(address))
			bt_sock.send(data)