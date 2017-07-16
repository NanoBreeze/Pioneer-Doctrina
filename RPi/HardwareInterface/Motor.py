
from Adafruit_MotorHAT import Adafruit_MotorHAT, Adafruit_DCMotor

''' 
Motor 1 and 2 are for driving forwards and backwards.
Motor 3 is from turning the wheels (servo motor)
'''



class Motor:
	def __init__(self):
		self.mh = Adafruit_MotorHAT(addr=0x60)
		self.rearMotor1 = self.mh.getMotor(1)
		self.rearMotor2 = self.mh.getMotor(2)
		self.servoMotor = self.mh.getStepper(100,3)
  
	def turnOffMotors():
		self.rearMotor1.run(Adafruit_MotorHAT.RELEASE)
		self.rearMotor2.run(Adafruit_MotorHAT.RELEASE)
		self.servoMotor.run(Adafruit_MotorHAT.RELEASE)
		self.servoMotor.setSpeed(20)

	def driveForward():
		self.rearMotor1.setSpeed(150)
		self.rearMotor2.setSpeed(150)
		self.rearMotor1.run(Adafruit_MotorHAT.FORWARD)
		self.rearMotor2.run(Adafruit_MotorHAT.FORWARD)
	

	def driveBackward():
		self.rearMotor1.setSpeed(150)
		self.rearMotor2.setSpeed(150)
		self.rearMotor1.run(Adafruit_MotorHAT.BACKWARD)
		self.rearMotor2.run(Adafruit_MotorHAT.BACKWARD)

	def turnLeft():
		# Not a stepper motor?
		self.servoMotor.step(10, Adafruit_MotorHAT.FORWARD, Adafruit_MotorHAT.SINGLE)
		
	def turnRight():
		self.servoMotor.step(10, Adafruit_MotorHAT.BACKWARD, Adafruit_MotorHAT.SINGLE)




