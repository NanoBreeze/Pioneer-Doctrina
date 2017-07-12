

class MovementController:
	DRIVE_FORWARD = 'W'
	DRIVE_FORWARD_LEFT = 'q'
	DRIVE_FORWARD_RIGHT = 'e'
	DRIVE_BACKWARD = 'S'
	DRIVE_BACKWARD_LEFT = 'z'
	DRIVE_BACKWARD_RIGHT = 'x'
	TURN_LEFT = 'A'
	TURN_RIGHT = 'D'

	def __init__(self, gpio):
		self.gpio = gpio
		self.allowedMovements = [
			self.DRIVE_FORWARD, self.DRIVE_FORWARD_LEFT,
			self.DRIVE_FORWARD_RIGHT, self.DRIVE_BACKWARD, 
			self.DRIVE_BACKWARD_LEFT, self.DRIVE_BACKWARD_RIGHT,
			self.TURN_LEFT, self.TURN_RIGHT ]

	def onReceiveMovement(self, movement):
		print("Inside onReceiveMovement. The movement is: " + movement)
		print(movement.encode('utf-8'))
		print(self.DRIVE_FORWARD.encode('utf-8'))
		if movement == 'W':
			print("Is equal to W")
		if movement in self.allowedMovements:
			print("About to call Gpio.move()")
			self.gpio.move(movement)
      
