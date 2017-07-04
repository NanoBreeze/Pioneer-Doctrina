import bluetooth # PyBluez

server_sock = bluetooth.BluetoothSocket(bluetooth.RFCOMM)
port = 1
server_sock.bind(("B8:27:EB:9C:80:A8",port))
server_sock.listen(1)

client_sock, address = server_sock.accept()
print "accepted connection from ", address

while True:
	data = client_sock.recv(1024)
	print "reeived [%s]" % data

server_sock.close()
