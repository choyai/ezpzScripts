#include <Event.h>
#include <Timer.h>

//RFID
#include <SPI.h>
#include <RFID.h>
#define SDA_DIO 9
#define RESET_DIO 8

//Timer
#include "Timer.h"

RFID RC522(SDA_DIO, RESET_DIO);
String recievedata;

int mode; // mode

//define pin
int ledred_pin = 2, ledgreen_pin = 3,ledblue_pin = 4; //led rgb pin
int fan_pin = 12; //fan pin
int vibra_pin1 = 12,vibra_pin2=13; //vibrating motor pin
int led1_pin = 22,led2_pin = 24,led3_pin = 26; //led pin
int bubble_pin = 28; //bubble pin
int uv_pin = 53; // uv pin
int b1_pin = 39,b2_pin = 41, b3_pin = 43, b4_pin = 45, b5_pin = 47, b6_pin = 49;


//define timer
unsigned long ledred_t,ledgreen_t,ledblue_t,fan_t,vibra_t,led1_t,led2_t,led3_t,bubble_t,uv_t;




void setup() {
  // put your setup code here, to run once:
  Serial.begin(57600); //baud rate = 57600

  //RFID set up
  SPI.begin();
  RC522.init();
  pinMode(b1_pin,OUTPUT);pinMode(b2_pin,OUTPUT);pinMode(b3_pin,OUTPUT);pinMode(b4_pin,OUTPUT);pinMode(b5_pin,OUTPUT);pinMode(b6_pin,OUTPUT);
  ledred_t = millis();ledgreen_t = millis(); ledblue_t = millis();fan_t = millis();vibra_t = millis();led1_t = millis();led2_t = millis(); led3_t = millis(); bubble_t = millis(); uv_t = millis();
  
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
      result = "Be";
    }
    else if(serNum == 51){
      result = "De";
    }
    else if(serNum == 165){
      result = "Li";
    }
    else if(serNum == 3){
      result = "El";
    }
    else if(serNum == 218){
      result = "Sn";
    }
    else if(serNum == 219){
      result = "Gi";
    }
    else if(serNum == 204){
      result = "Rh";
    }
    else if(serNum == 11){
      result = "Sh";
    }
    else if(serNum == 103){
      result = "Mo";
    }
    else if(serNum == 26){
      result = "Sq";
    }

    return result;  
}

void loop() {
  // put your main code here, to run repeatedly:
//  if (Serial.available()){
//    recievedata = Serial.read();
//  }
//  
//  if (mode==1){
//    if (RC522.isCard()){
//      RC522.readCardSerial();
//      Serial.println("Card detected:");
//      for(int i=0;i<5;i++)
//      {
//      Serial.print(RC522.serNum[i],DEC);
//      //Serial.print(RC522.serNum[i],HEX); //to print card detail in Hexa Decimal format
//      }
//      Serial.println();
//      Serial.println();
//       }
//      delay(1000);
//    }
//  else if (mode == 2){
//  }
//  else if (mode == 3){
//    
//  }

  if (RC522.isCard()){
    RC522.readCardSerial();
    Serial.println(rfid_check(RC522.serNum[1]));
    delay(1000);
  }


      
}
