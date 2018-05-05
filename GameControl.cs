using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using System;

public class GameControl : MonoBehaviour
{
public static int SceneCount = 0;
public static string CurrentAnimal = "";
public static bool Button1 = false, Button2 = false, Button3 = false, Button4 = false, Button5 = false;
public static int Button1Count = 0, Button2Count = 0, Button3Count = 0, Button4Count = 0, Button5Count = 0;
public static string[] animals = new string[]
{

        "Bear",
        "Squirrel",
        "Sheep",
        //"Monkey",
        "Rhino",
        "Giraffe",
        "Snake",
        "Deer",
        "Lion",
        "Elephant"
};

public static List<string> Animals = new List<string>(animals);
public SerialController serialController;
// Use this for initialization
void OnEnable()
{
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        //DontDestroyOnLoad(this.gameObject);
}


// Update is called once per frame
void Update()
{
        string message = serialController.ReadSerialMessage();


        if (message == null)
                return;


        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
        else
        {  Debug.Log("Message arrived: " + message);
           InputHandler(message);}
}

private void OnGUI()
{
        GUILayout.Label("Press Enter To Advance");
}

public void InputHandler(string data)
{
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
