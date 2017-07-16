# Pioneer Doctrina

Pioneer Doctrina is a Raspberry Pi-based car that is controlled with a Windows App. I built it to learn about the Raspberry Pi
and gain some hands-on experience with embedded systems. There remains  

The Windows App sends commands via TCP/IP or RFCOMM to control the car's movements - drive forward, backwards, left, and right - when the user presses
the user presses the "Up", "Down", "Left", and "Right" keys. 

The car contains the Raspberry Pi Camera Module v2 to stream videos to the Windows App, which enables the user to capture and save video frames.

To enable the car to be controlled from the Windows App and stream videos, run ```$python RPi/main.py```
