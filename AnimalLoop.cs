﻿using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class AnimalLoop : MonoBehaviour
{
public SerialController serialController;
// Use this for initialization
void OnEnable()
{
        GameControl.Button1Count = 0;
        GameControl.Button2Count = 0;
        GameControl.Button3Count = 0;
        GameControl.Button4Count = 0;
        GameControl.Button5Count = 0;
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
}

// Update is called once per frame
void Update()
{
        if (GameControl.Button1Count > 0)  //&& GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
        {
                SceneManager.LoadScene("Randomizer");
        }
        string message = serialController.ReadSerialMessage();

        if (message == null)
                return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
        else
                Debug.Log("Message arrived: " + message);
        InputHandler(message);
}
private void OnGUI()
{
        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
                Debug.Log("please");
                serialController.SendSerialMessage("b1");
                //WriteToArduino("button2press");
                //WriteToArduino("button3press");
                //WriteToArduino("button4press");
                //WriteToArduino("button5press");
                // if (GameControl.Button1Count > 0) /*&& GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)*/
                // {
                // SceneManager.LoadScene("Randomizer");
                // }
        }
}



// public async Task AsyncReadFromArduino(Action<string> callback){
//         DateTime initialTime = DateTime.Now;
//         DateTime nowTime;
//         TimeSpan diff = default(TimeSpan);
//
//         string dataString = null;
//
//         do
//         {
//                 try
//                 {
//                         dataString = GameControl.stream.ReadLine();
//                 }
//                 catch (Exception ex)
//                 {
//                         throw ex;
//                 }
//
//                 if (dataString != null)
//                 {
//                         callback(dataString);
//                 }
//                 else
//                         await Task.Delay(TimeSpan.FromSeconds(0.05f));
//
//                 nowTime = DateTime.Now;
//                 diff = nowTime - initialTime;
//
//         } while (diff.Milliseconds < GameControl.stream.ReadTimeout);
// }
// public IEnumerator CoReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
// {
//         DateTime initialTime = DateTime.Now;
//         DateTime nowTime;
//         TimeSpan diff = default(TimeSpan);
//
//         string dataString = null;
//
//         do
//         {
//                 try
//                 {
//                         dataString = GameControl.stream.ReadLine();
//                         Debug.Log(dataString);
//                 }
//                 catch (TimeoutException)
//                 {
//                         dataString = null;
//                 }
//
//                 if (dataString != null)
//                 {
//                         callback(dataString);
//                         yield return null;
//                 }
//                 else
//                         yield return new WaitForSeconds(0.1f);
//
//                 nowTime = DateTime.Now;
//                 diff = nowTime - initialTime;
//
//         } while (diff.Milliseconds < timeout);
//
//         if (fail != null)
//                 fail();
//         yield return null;
// }
// public void WriteToArduino(string message)
// {
//         GameControl.stream.WriteLine(message);
//         GameControl.stream.BaseStream.Flush();
// }
public void InputHandler(string data)
{
        Debug.Log("yo");
        Debug.Log(data);
        //GameControl.Button1Count = data[0];
        //GameControl.Button2Count = data[1];
        //GameControl.Button3Count = data[2];
        //GameControl.Button4Count = data[3];
        //GameControl.Button5Count = data[4];
        switch (data[0])
        {
        case 'b':
                switch (data[1])
                {
                case '1':
                        GameControl.Button1Count += 1;
                        break;
                case '2':
                        GameControl.Button2Count += 1;
                        break;
                case '3':
                        GameControl.Button3Count += 1;
                        break;
                case '4':
                        GameControl.Button4Count += 1;
                        break;
                case '5':
                        GameControl.Button5Count += 1;
                        break;
                }
                break;
        }
}
}
