void setup() {
  // put your setup code here, to run once:
  Serial.begin(57600);
  
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available())
  {
    Serial.println(Serial.read());
  }
}
