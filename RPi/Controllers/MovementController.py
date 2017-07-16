

class MovementController:
	DRIVE_FORWARD = 'W'
	DRIVE_FORWARD_LEFT = 'q'
	DRIVE_FORWARD_RIGHT = 'e'
	DRIVE_BACKWARD = 'S'
	DRIVE_BACKWARD_LEFT = 'z'
	DRIVE_BACKWARD_RIGHT = 'x'
	TURN_LEFT = 'A'
	TURN_RIGHT = 'D'

	def __init__(self, motor):
		self.motor = motor
		self.allowedMovements = [
			self.DRIVE_FORWARD, self.DRIVE_FORWARD_LEFT,
			self.DRIVE_FORWARD_RIGHT, self.DRIVE_BACKWARD, 
			self.DRIVE_BACKWARD_LEFT, self.DRIVE_BACKWARD_RIGHT,
			self.TURN_LEFT, self.TURN_RIGHT ]

	def onReceiveMovement(self, movement):
		if movement in self.allowedMovements:
			if movement == self.DRIVE_FORWARD:
				self.motor.driveForward()
			elif movement == self.DRIVE_FORWARD_LEFT:
				self.motor.driveForward()
				self.motor.turnLeft()
			elif movement == self.DRIVE_FORWARD_RIGHT:
				self.motor.driveForward()
				self.motor.turnRight()
			elif movement == self.DRIVE_BACKWARD:
				self.motor.driveBackward()
			elif movement == self.DRIVE_BACKWARD_LEFT:
				self.motor.driveBackward()
				self.motor.turnLeft()
			elif movement == self.DRIVE_BACKWARD_RIGHT:
				self.motor.driveBackward()
				self.motor.turnRight()
			elif movement == self.TURN_LEFT:
				self.motor.turnLeft()
			else:
				self.motor.turnRight()
