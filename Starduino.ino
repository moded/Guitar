#include<SoftwareSerial.h>
#include "FastLED.h"

using namespace std;

#define BRIGHTNESS  150

// Define the array of leds
CRGB leds0[6];
CRGB leds1[6];
CRGB leds2[6];
CRGB leds3[6];
CRGB leds4[6];
CRGB leds5[6];

int row[6] = {0};
int d[4] = {2, 3, 4, 5};
int count = 8;

bool buttons[72] = {0};
int chord = 0;

void CheckNextFret();
void playChord(char c);
void clearLeds();
void SetTheme(char c);
bool chordWasPlayed();
void playSong(char c);

String c = "";
CRGB col;

void setup() {

  //Set Select pins for MUX 
  for (int i = 0; i < 4; i++)
    pinMode(d[i], OUTPUT);

  Serial.begin(9600);
  
  //Init Fastled variables
     FastLED.addLeds<NEOPIXEL, 6>(leds0,6);
     FastLED.addLeds<NEOPIXEL, 7>(leds1,6);
     FastLED.addLeds<NEOPIXEL, 8>(leds2,6);
     FastLED.addLeds<NEOPIXEL, 9>(leds3,6);
     FastLED.addLeds<NEOPIXEL, 10>(leds4,6);
     FastLED.addLeds<NEOPIXEL, 11>(leds5,6);
     
     FastLED.setBrightness( BRIGHTNESS );
}

void loop() {

  CheckNextFret();
  
  for (int i = 0; i < 6; i++){
    row[i] = analogRead(i);
    if (row[i] > 512 ){
      buttons[(count-8)*6+i] = true;
    }
    else 
    {
      buttons[(count-8)*6+i] = false;
    }
  }
   count = (count-7)%4 + 8;
   
  if(Serial.available()){
    c = Serial.readString();
    if(c.c_str()[0] == '#'){
      col = CRGB(rand()%255,rand()%255,rand()%255);
      playChord(c.c_str()[1]);
    }
    else if(c.c_str()[0] =='$'){
      SetTheme(c.c_str()[1]);
    }
    else if(c.c_str()[0] == '@'){
      playSong(c.c_str()[1]);
    }
  }
  else if ( c != "" )
  {
    int x = chord;
    if(c.c_str()[0] == '#'){
      playChord(c.c_str()[1]);
    }
    else if(c.c_str()[0] =='$'){
      SetTheme(c.c_str()[1]);
    }
    chord = x;
  }
  
  if(chordWasPlayed() && chord != 0)
  {
    Serial.println("ok");
    delay(20);
    chord = 0;
  }
}

void CheckNextFret(){
  switch (count){
    case 0:
      digitalWrite(d[0], LOW);
      digitalWrite(d[1], LOW);
      digitalWrite(d[2], LOW);
      digitalWrite(d[3], LOW);
      break;
    case 1:
      digitalWrite(d[0], HIGH);
      digitalWrite(d[1], LOW);
      digitalWrite(d[2], LOW);
      digitalWrite(d[3], LOW);
      break;
    case 2:
      digitalWrite(d[0], LOW);
      digitalWrite(d[1], HIGH);
      digitalWrite(d[2], LOW);
      digitalWrite(d[3], LOW);
      break;
    case 3:
      digitalWrite(d[0], HIGH);
      digitalWrite(d[1], HIGH);
      digitalWrite(d[2], LOW);
      digitalWrite(d[3], LOW);
      break;
    case 4:
      digitalWrite(d[0], LOW);
      digitalWrite(d[1], LOW);
      digitalWrite(d[2], HIGH);
      digitalWrite(d[3], LOW);
      break;
    case 5:
      digitalWrite(d[0], HIGH);
      digitalWrite(d[1], LOW);
      digitalWrite(d[2], HIGH);
      digitalWrite(d[3], LOW);
      break;
    case 6:
      digitalWrite(d[0], LOW);
      digitalWrite(d[1], HIGH);
      digitalWrite(d[2], HIGH);
      digitalWrite(d[3], LOW);
      break;
    case 7:
      digitalWrite(d[0], HIGH);
      digitalWrite(d[1], HIGH);
      digitalWrite(d[2], HIGH);
      digitalWrite(d[3], LOW);
      break;
    case 8:
      digitalWrite(d[0], LOW);
      digitalWrite(d[1], LOW);
      digitalWrite(d[2], LOW);
      digitalWrite(d[3], HIGH);
      break;
    case 9:
      digitalWrite(d[0], HIGH);
      digitalWrite(d[1], LOW);
      digitalWrite(d[2], LOW);
      digitalWrite(d[3], HIGH);
      break;
    case 10:
      digitalWrite(d[0], LOW);
      digitalWrite(d[1], HIGH);
      digitalWrite(d[2], LOW);
      digitalWrite(d[3], HIGH);
      break;
    case 11:
      digitalWrite(d[0], HIGH);
      digitalWrite(d[1], HIGH);
      digitalWrite(d[2], LOW);
      digitalWrite(d[3], HIGH);
      break;
  }
}

void clearLeds(){
  for(int i=0;i<6;i++)
    leds0[i] = CRGB::Black;
  for(int i=0;i<6;i++)
    leds1[i] = CRGB::Black;
  for(int i=0;i<6;i++)
    leds2[i] = CRGB::Black;
  for(int i=0;i<6;i++)
    leds3[i] = CRGB::Black;
  for(int i=0;i<6;i++)
    leds4[i] = CRGB::Black;
  for(int i=0;i<6;i++)
    leds5[i] = CRGB::Black;
}

void playChord(char c){
  clearLeds();
    switch(c){
      case 'A' : leds4[3] = col;
                 leds4[2] = col;
                 leds4[1] = col;
                 chord  = 1;
                 break; //A
      case 'a' : leds5[1] = col; 
            leds4[3] = col; 
            leds4[2] = col;
            chord  = 2;
            break; //Am
      case 'B' :
            for(int i=0;i<5;i++)
              leds4[i] = col;
            for(int i=1;i<4;i++)
              leds2[i] = col;
              chord  = 3;
              break; //B
      case 'b' :
            for(int i=0;i<5;i++)
              leds4[i] = col;
            leds3[1] = col;
            leds2[3] = col;
            leds2[2] = col;
            chord  = 4; 
          break; //Bm   
      case 'C' :  
            leds5[1] = col;
            leds4[3] = col; 
            leds3[4] = col;
            chord  = 5;
            break; //C
      case 'c' :
            leds4[1] = col;
            leds3[3] = col;
            leds2[4] = col;
            chord = 6;
            break;
      case 'D' :  
            leds4[2] = col; 
            leds4[0] = col;  
            leds3[1] = col;
            chord  = 7;
            break; //D
      case 'd' :  
            leds5[0] = col; 
            leds4[2] = col; 
            leds3[1] = col;
            chord  = 8;
            break; //Dm
      case 'E' :  
            leds5[2] = col;
            leds4[4] = col;
            leds4[3] = col;
            chord  = 9;
            break; //E
      case 'e' :  
            leds4[4] = col;
            leds4[3] = col;
            chord  = 10;
            break; //Em
      case 'F' :  
            for(int i=0;i<6;i++)
              leds5[i] = col;
            leds4[2] = col;
            leds3[4] = col;
            leds3[3] = col;
            chord  = 11;
            break; //F
      case 'f' :   
            for(int i=0;i<6;i++)
              leds5[i] = col;
            leds3[4] = col;
            leds3[3] = col;
            chord  = 12;
            break; //Gm
      case 'G' :  
            leds4[4] = col; 
            leds3[5] = col; 
            leds3[0] = col;
            chord  = 13;
            break; //G
      case 'g' : // f# 
            for(int i=0;i<6;i++)
              leds4[i] = col;
            leds3[2] = col;
            leds2[4] = col;
            leds2[3] = col;
            chord  = 14;
            break;
    }
    FastLED.show();
}

void SetTheme(char c){
  clearLeds();
    switch(c){
      case 'A' : 
        for(int i=0;i<6;i++){
          leds0[i] = CRGB::Red;
          leds1[i] = CRGB::Red;
          leds2[i] = CRGB::Red;
          leds3[i] = CRGB::Red;
          leds4[i] = CRGB::Red;
          leds5[i] = CRGB::Red;
        }
       break;
       case 'B' :
          static uint8_t starthue = 0;
          fill_rainbow( leds0, 6, --starthue, 20);
          fill_rainbow( leds1, 6, --starthue, 20);
           fill_rainbow( leds2, 6, --starthue, 20);
          fill_rainbow( leds3, 6, --starthue, 20);
           fill_rainbow( leds4, 6, --starthue, 20);
          fill_rainbow( leds5, 6, --starthue, 20);
        break;
        case 'C' :
          leds5[1] = CRGB::Red;
          leds5[2] = CRGB::Red;
          leds4[0] = CRGB::Red;
          leds4[3] = CRGB::Red;
          leds4[4] = CRGB::Red;
           
          leds3[0] = CRGB::Red;
          leds3[3] = CRGB::Red;
          leds3[4] = CRGB::Red;
          leds2[1] = CRGB::Red;
          leds2[2] = CRGB::Red;
          break;
      }
    FastLED.show();
}

bool chordWasPlayed()
{
  switch(chord)
  {
    case 1: //A
      if(buttons[20]&& buttons[21])
        return true;
      break;
    case 2: //Am
      if(buttons[13] && buttons[20]&& buttons[21])
        return true;
      break;
    case 3: //B
      if(buttons[1] && buttons[2] && buttons[3])
        return true;
      break;
    case 4: //Bm
      if(buttons[7] && buttons[2] && buttons[3])
        return true;
      break;
    case 5: // C
      if(buttons[13] && buttons[21] && buttons[10] )
        return true;
      break;
    case 7: //D
      if(buttons[18] && buttons[20]&& buttons[7])
        return true;
      break;
    case 8: //Dm
      if(buttons[12] && buttons[7] && buttons[20])
        return true;
      break;
    case 9: //E
      if(buttons[14])
        return true;
      break;
    case 10: //Em
      if(buttons[22])
        return true;
      break;
    case 11: //F
      if(buttons[9] && buttons[10] && buttons[20] )
        return true;
      break;
    case 12: // Fm
      if(buttons[9] && buttons[10] )
        return true;
      break;  
    case 13: //G
      if(buttons[22] && buttons[11] && buttons[6] )
        return true;
      break;
    default :  break;
  }
  return false;
}

void playSong(char c)
{
  if(c == 'A')
  {
    for(int i=0;i<10;i++)
    {
      playChord('B');
      delay(4000);
      playChord('g');
      delay(2000);
      playChord('c');
      delay(2000); 
    }
      playChord('B');
      delay(4000);
      playChord('g');
      delay(2000);
  }
  else if ( c == 'B')
  {
      playChord('G');
      delay(5000);
      
      for( int j=0;j<4;j++)
      {
        playChord('G');
        delay(1250);
        playChord('C');
        delay(1250);
        playChord('D');
        delay(2500);
      }
      for( int j=0;j<2;j++)
      {
        playChord('e');
        delay(1250);
        playChord('D');
        delay(1250);
        playChord('G');
        delay(1250);
        playChord('C');
        delay(1250);
      }
      for( int j=0;j<2;j++)
      {
        playChord('e');
        delay(1250);
        playChord('G');
        delay(1250);
      }
      playChord('e');
        delay(1250);
        playChord('D');
        delay(1250);
      for( int j=0;j<4;j++)
      {
        playChord('G');
        delay(2500);
        playChord('C');
        delay(1250);
        playChord('D');
        delay(1250);
      }
       for( int j=0;j<2;j++)
      {
        playChord('e');
        delay(1250);
        playChord('D');
        delay(1250);
        playChord('G');
        delay(1250);
        playChord('C');
        delay(1250);
      }
      for( int j=0;j<2;j++)
      {
        playChord('e');
        delay(1250);
        playChord('G');
        delay(1250);
      }
      playChord('e');
        delay(1250);
        playChord('D');
        delay(1250);
      for( int j=0;j<2;j++)
      {
        playChord('G');
        delay(2500);
        playChord('C');
        delay(1250);
        playChord('D');
        delay(1250);
      }
        playChord('e');
        delay(2500);
        playChord('C');
        delay(1250);
        playChord('D');
        delay(1250);
        playChord('G');
        delay(2500);
        playChord('C');
        delay(1250);
        playChord('D');
        delay(1250);
        for(int j=0;j<2;j++)
        {
          playChord('e');
          delay(1250);
          playChord('D');
          delay(1250);
          playChord('C');
          delay(1250);
          playChord('G');
          delay(1250);
        }
        for( int i=0;i<2;i++)
        {
          for( int j=0;j<2;i++)
          {
            playChord('e');
            delay(1250);
            playChord('G');
            delay(1250);
          }
          playChord('e');
            delay(1250);
            playChord('D');
            delay(1250);
          for( int j=0;j<2;i++)
          {
            playChord('G');
            delay(2500);
            playChord('C');
            delay(1250);
            playChord('D');
            delay(1250);
          }
        }
  }
}

