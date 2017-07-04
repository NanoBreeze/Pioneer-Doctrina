import socket
from socket import *

serverSocket = socket(AF_INET, SOCK_DGRAM)
serverSocket.bind(('10.0.0.109', 1337))

while True:
   message, address = serverSocket.recvfrom(1024)
   print(message.decode())
