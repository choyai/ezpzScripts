﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using System;

public class GameControl : MonoBehaviour
{
    public static SerialPort stream = new SerialPort("COM7", 9600);
    public static int SceneCount = 0;
    public static string CurrentAnimal = "";
    public static bool Button1 = false, Button2 = false, Button3 = false, Button4 = false, Button5 = false;
    public static int Button1count = 0, Button2count = 0, Button3count = 0, Button4count = 0, Button5count = 0;
    public static string[] animals = new string[]
    {
        "Bear",
        //"Squirrel",
        //"Sheep",
        //"Monkey",
        "Rhino",
        //"Giraffe",
        //"Snake",
        //"Deer",
        //"Lion",
        //"Elephant"
    };

    public static List<string> Animals = new List<string>(animals);

    // Use this for initialization
    void Start()
    {
        stream.ReadTimeout = 50;
        stream.Open();
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine
        (
                AsynchronousReadFromArduino
                        ((string s) => InputHandler(s), // Callback
                        () => Debug.LogError("Error!"), // Error callback
                        10000f                      // Timeout (milliseconds)
                        )
        );
    }

    private void OnGUI()
    {
        GUILayout.Label("Press Enter To Advance");
    }

    public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        string dataString = null;

        do
        {
            try
            {
                dataString = stream.ReadLine();
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                callback(dataString);
                yield return null;
            }
            else
                yield return new WaitForSeconds(0.05f);

            nowTime = DateTime.Now;
            diff = nowTime - initialTime;

        } while (diff.Milliseconds < timeout);

        if (fail != null)
            fail();
        yield return null;
    }
    public void WriteToArduino(string message)
    {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }
    public void InputHandler(string data)
    {
        Debug.Log(data);
        if (data == "button1press")
        {
            Button1count += 1;
        }
        else if (data == "button2press")
        {
            Button2count += 1;
        }
        else if (data == "button3press")
        {
            Button3count += 1;
        }
        else if (data == "button4press")
        {
            Button4count += 1;
        }
        else if (data == "button5press")
        {
            Button5count += 1;
        }

    }
}
