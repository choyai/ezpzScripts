using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.SceneManagement;
using System;

public class GameControl : MonoBehaviour
{
    public static SerialPort stream = new SerialPort("COM6", 9600);
    public static int SceneCount = 0;
    public static string CurrentAnimal = "";
    public static bool Button1 = false, Button2 = false, Button3 = false, Button4 = false, Button5 = false;
    public static int Button1Count = 0, Button2Count = 0, Button3Count = 0, Button4Count = 0, Button5Count = 0;
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
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine
        (
                AsynchronousReadFromArduino
                        ((s) => InputHandler(s), // Callback
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
        //Debug.Log(data);
        switch (data)
        {
            case "button1press":
                Button1Count += 1;
                Button1 = true;
                break;
            case "button2press":
                Button2Count += 1;
                Button2 = true;
                break;
            case "button3press":
                Button3Count += 1;
                Button3 = true;
                break;
            case "button4press":
                Button4Count += 1;
                Button4 = true;
                break;
            case "button5press":
                Button5Count += 1;
                Button5 = true;
                break;
        }

    }
}
