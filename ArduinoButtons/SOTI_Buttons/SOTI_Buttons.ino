//We have an Input and an Output for each button.
//Here the Input for the Buttons
int greenButton = 5;
int redButton = 7;
int blueButton = 9;

//Led Output for the Buttons
int redLed = 6;
int greenLed = 8;
int blueLed = 10;

//Constant string for the serial communication
String GREEN = "1";
String RED = "2";
String BLUE = "3";
String OFF_ALL = "0";

//Memory of states
int STATE_G = LOW;
int STATE_R = LOW;
int STATE_B = LOW;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  //PushButtons
  pinMode(redButton, INPUT);
  pinMode(greenButton, INPUT);
  pinMode(blueButton, INPUT);
  //Leds
  pinMode(redLed, OUTPUT);
  pinMode(greenLed, OUTPUT);
  pinMode(blueLed, OUTPUT);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  serialEvent();
  
  int redState = digitalRead(redButton);
  int greenState = digitalRead(greenButton);
  int blueState = digitalRead(blueButton);
  bool pressed = false;
  
  if (greenState == HIGH){
    Serial.println(GREEN);
    pressed = true;
  }else if (redState == HIGH){
    Serial.println(RED);
    pressed = true;
  }else if (blueState == HIGH){
    Serial.println(BLUE);
    pressed = true;
  }

  if (pressed){
    delay(1000);
  }
}

/*
 * Simply turn off all the leds.
 */
void ledsOff(){
  digitalWrite(greenLed, LOW);
  digitalWrite(redLed, LOW);
  digitalWrite(blueLed, LOW);
}


/*
 * Read from the serial port and turns ON/OFF the requested leds.
 */
void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    String inChar = Serial.readString();
    
    if (inChar == GREEN) {
      digitalWrite(greenLed, HIGH);
    }else if (inChar == RED){
      digitalWrite(redLed, HIGH);
    }else if (inChar == BLUE){
      digitalWrite(blueLed, HIGH);
    }else if (inChar == OFF_ALL){
      ledsOff();
    }
  }
}
