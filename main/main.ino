
//RFID
#include <SPI.h>
#include <RFID.h>
#define SDA_DIO 9
#define RESET_DIO 8

//Timer


RFID RC522(SDA_DIO, RESET_DIO);
String receivedata;

int mode = 3; // mode

//define pin
int ledred_pin = 2, ledgreen_pin = 3,ledblue_pin = 4; //led rgb pin
int fan_pin = 34; //fan pin
int led1_pin = 22,led2_pin = 24,led3_pin = 26; //led pin
int bubble_pin = 28; //bubble pin
int uv_pin = 40; // uv pin
int vi_pin = 30;
int b1_pin = 39,b2_pin = 41, b3_pin = 43, b4_pin = 45, b5_pin = 47, b6_pin = 49;
int output_pin[10] = {ledred_pin, ledgreen_pin,ledblue_pin,fan_pin,led1_pin,led2_pin,led3_pin,bubble_pin,uv_pin,vi_pin};
int lightmap[11] = {14,15,16,17,18,19,20,21,23,25,27};
int button[6] = {b1_pin,b2_pin,b3_pin,b4_pin,b5_pin,b6_pin};



int b1,b2,b3,b4,b5,b6;

//define timer
unsigned long t;

int exitloop;
int switch_off = 0;
int exitAnimalsLoop = 0;


void setup() {
  // put your setup code here, to run once:
  Serial.begin(57600); //baud rate = 57600
  //RFID set up
  SPI.begin();
  RC522.init();
  for(int i=0;i<6;i++){
    pinMode(button[i], INPUT);
  } 
  for(int i=0;i<=9;i++){
    pinMode(output_pin[i],OUTPUT);
  }
  for(int i=0;i<11;i++){
    pinMode(lightmap[i],OUTPUT);
  }
  t = millis();
  b1 = digitalRead(b1_pin);
  b2 = digitalRead(b2_pin);
  b3 = digitalRead(b3_pin);
  b4 = digitalRead(b4_pin);
  b5 = digitalRead(b5_pin);
  
}

/*
 * receive q: rfid request  
 * receive s: stop command 
 *
 * Serial out;
 * b1 = button 1 pressed
 * b2 = button 2 pressed
 * b3 = button 3 pressed
 * b4 = button 4 pressed
 * b5 = button 5 pressed
 * 
 */
 String rfid_check(int serNum){
  String result;

    RC522.readCardSerial();
    if (serNum == 254){
      result = "Bear";
    }
    else if(serNum == 51){
      result = "Deer";
    }
    else if(serNum == 165){
      result = "Lion";
    }
    else if(serNum == 3){
      result = "Elephant";
    }
    else if(serNum == 218){
      result = "Snake";
    }
    else if(serNum == 219){
      result = "Giraffe";
    }
    else if(serNum == 204){
      result = "Rhino";
    }
    else if(serNum == 11){
      result = "Sheep";
    }
    else if(serNum == 103){
      result = "Monkey";
    }
    else if(serNum == 26){
      result = "Squirrel";
    }

    return result;  
}
/*
 * mode 0 rfid
 * mode 1 animals
 * mode 2 random map
 * mode 3 interval
 */

 // mode 2
unsigned long tlight;
unsigned long tlmap;
int lighton;
int x;

// mode 3
unsigned long tr,tg,tb,tv,tf,te,tu;

int red_blink;
int red_on;

int wrong = 0;
int correct = 0;
void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available()>0){
    receivedata = Serial.read();
    mode = receivedata.toInt()- 48;
//    Serial.println(mode);
    
    if (mode == 1){
      exitAnimalsLoop = 0;
    }
    if (mode == 2){
      tlight = millis();
      tlmap = millis();
      lighton = 1;
      x = 0;
    }
  }
  if (mode == 0){
    exitloop = 0;
    while(exitloop == 0){
//      Serial.println("Mode 0");
      if(Serial.available()>0){
        char data = Serial.read();
        if (data == 'i'){
          wrong = 1;
          t = millis();
          x = 0;
          digitalWrite(ledred_pin,HIGH);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(vi_pin,HIGH);
          
        }
        else if (data == 'c'){
//          digitalWrite(ledgreen_pin,HIGH);
//          digitalWrite(led1_pin,HIGH);
//          digitalWrite(led2_pin,HIGH);
//          digitalWrite(led2_pin,HIGH);
//          digitalWrite(vi_pin,HIGH);
//          delay(1000);
//          digitalWrite(ledgreen_pin,LOW);
//          digitalWrite(led1_pin,LOW);
//          digitalWrite(led2_pin,LOW);
//          digitalWrite(led2_pin,LOW);
          exitloop = 1;
          mode = 3;
        }
      }
      if (RC522.isCard()){
        RC522.readCardSerial();
        Serial.println(rfid_check(RC522.serNum[1]));
//        delay(600);
      }
      if (wrong == 1 && millis()-t >=300){
        if (x == 0){
          digitalWrite(ledred_pin,HIGH);
          digitalWrite(led1_pin,LOW);
          digitalWrite(led2_pin,LOW);
          digitalWrite(led2_pin,LOW);
          digitalWrite(vi_pin,LOW);
        }
        else if (x == 1){
          digitalWrite(ledred_pin,HIGH);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(vi_pin,HIGH);
        }
        else if (x == 2){
          digitalWrite(ledred_pin,LOW);
          digitalWrite(led1_pin,LOW);
          digitalWrite(led2_pin,LOW);
          digitalWrite(led2_pin,LOW);
          digitalWrite(vi_pin,LOW);
        }
        t = millis();
        x++;
        if(x ==2){
          wrong =0;
        }
      }
      
    }
  }
  else if (mode == 1){
    while(exitAnimalsLoop  == 0){
//      Serial.println("Mode 1");
      if(Serial.available()>0){
        char data = Serial.read();
        if (data =='R'){
          digitalWrite(ledred_pin,HIGH);
          digitalWrite(ledgreen_pin,LOW);
          digitalWrite(ledblue_pin,LOW);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
        }
        else if (data == 'r'){                 
          digitalWrite(ledred_pin,LOW);
          digitalWrite(ledgreen_pin,LOW);
          digitalWrite(ledblue_pin,LOW);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
        }
        else if (data == 'P'){
          red_blink = 1;
          digitalWrite(ledred_pin,HIGH);
          digitalWrite(ledgreen_pin,LOW);
          digitalWrite(ledblue_pin,LOW);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          red_on = 1;
          tr = millis();
        }
        else if (data == 'p'){
          red_blink = 0;
        }
        else if (data == 'G'){
          digitalWrite(ledred_pin,LOW);
          digitalWrite(ledgreen_pin,HIGH);
          digitalWrite(ledblue_pin,LOW);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
        } 
        else if (data == 'g'){
          digitalWrite(ledred_pin,LOW);              
          digitalWrite(ledgreen_pin,LOW);
          digitalWrite(ledblue_pin,LOW);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
        }
        else if (data == 'B'){
          digitalWrite(ledred_pin,LOW);
          digitalWrite(ledgreen_pin,LOW);
          digitalWrite(ledblue_pin,HIGH);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
        }
         else if (data == 'b'){
          digitalWrite(ledblue_pin,LOW);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
        }
        else if (data == 'W'){
          digitalWrite(ledred_pin,HIGH);
          digitalWrite(ledgreen_pin,HIGH);
          digitalWrite(ledblue_pin,HIGH);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
        }
        
        else if (data == 'w'){
          digitalWrite(ledred_pin,LOW);
          digitalWrite(ledgreen_pin,LOW);
          digitalWrite(ledblue_pin,LOW);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
        }
        
        else if (data == 'V'){
          digitalWrite(vi_pin,HIGH);
        }
         else if (data == 'V'){
          digitalWrite(vi_pin,LOW);
        }
        else if (data == 'F'){
          digitalWrite(fan_pin,HIGH);

        }
        else if (data == 'f'){
          digitalWrite(fan_pin,LOW);
        }
        else if (data == 'E'){        //bubble
           digitalWrite(bubble_pin,HIGH);
        }
        else if (data == 'e'){        //bubble
           digitalWrite(bubble_pin,LOW);
        }
        else if (data == 'U'){
          digitalWrite(uv_pin,HIGH);
        }
        else if (data == 'u'){
          digitalWrite(uv_pin,LOW);
        }
        else if (data == 's'){
          exitAnimalsLoop = 1;
          mode = 3;
        }
      }
      if (millis()-tr >= 300 && red_blink ==1){
        if (red_on == 0){
          digitalWrite(ledred_pin,HIGH);
          digitalWrite(ledgreen_pin,LOW);
          digitalWrite(ledblue_pin,LOW);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          red_on = 1;
        }
        else if (red_on == 1){
          digitalWrite(ledred_pin,LOW);
          digitalWrite(ledgreen_pin,LOW);
          digitalWrite(ledblue_pin,LOW);
          digitalWrite(led1_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          digitalWrite(led2_pin,HIGH);
          red_on = 0;
        }
        tr = millis();
      }
    }
  }
  else if (mode == 2){
//    Serial.println("mode 2");
    if(millis()-tlight >=1000){
      if(lighton == 1){
        digitalWrite(ledred_pin,HIGH);
        digitalWrite(ledgreen_pin,HIGH);
        digitalWrite(ledblue_pin,HIGH);
        digitalWrite(led1_pin,HIGH);
        digitalWrite(led2_pin,HIGH);
        digitalWrite(led3_pin,HIGH);
        lighton = 0;
      }
      
      else if (lighton == 0){
         digitalWrite(ledred_pin,LOW);
         digitalWrite(ledgreen_pin,LOW);
         digitalWrite(ledblue_pin,LOW);
         digitalWrite(led1_pin,LOW);
         digitalWrite(led2_pin,LOW);
         digitalWrite(led3_pin,LOW);
         lighton = 1;
      }
    }
    if(millis() - tlmap >= 300){
      for(int i; i<11; i++){
        digitalWrite(lightmap[i],LOW);
      }
      digitalWrite(lightmap[x],HIGH);
      x++;
      if(x>10){
        x = 0;
      }
      tlmap = millis();
   }
      
  }
  else if (mode == 3){
//    Serial.println("Mode 3");
    if(switch_off == 0){
      for(int i = 0;i<=9;i++){
        digitalWrite(output_pin[i],LOW);
      }
      switch_off = 1;
    }
    
  }
 
  if(digitalRead(b1_pin)<b1){
    Serial.println("b1");
  }
  if(digitalRead(b2_pin)<b2){
    Serial.println("b2");
  }
  if(digitalRead(b3_pin)<b3){
    Serial.println("b3");
  }
  if(digitalRead(b4_pin)<b4){
    Serial.println("b4");
  }
  if(digitalRead(b5_pin)< b5){
    Serial.println("b5");
  }
  
  b1 = digitalRead(b1_pin);
  b2 = digitalRead(b2_pin);
  b3 = digitalRead(b3_pin);
  b4 = digitalRead(b4_pin);
  b5 = digitalRead(b5_pin);
  

}
