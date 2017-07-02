import socket

s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

host = '127.0.0.1'
port = 1337

while(1):
   print("Trying to send hello world")
   s.sendto(b'hello world', (host, port))
   
