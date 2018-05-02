using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AnimalLoop : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine
        //(
        //        AsynchronousReadFromArduino
        //                ((s) => InputHandler(s), // Callback
        //                () => Debug.LogError("Error!"), // Error callback
        //                10000f                      // Timeout (milliseconds)
        //                )
        //);
        Debug.Log(GameControl.Button1Count);
    }
    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
            WriteToArduino("button1press");
            WriteToArduino("button2press");
            WriteToArduino("button3press");
            WriteToArduino("button4press");
            WriteToArduino("button5press");
            if (GameControl.Button1Count > 0 && GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
            {
                GameControl.SceneCount++;
                SceneManager.LoadScene("Randomizer");
            }
        }
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
                dataString = GameControl.stream.ReadLine();
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
        GameControl.stream.WriteLine(message);
        GameControl.stream.BaseStream.Flush();
    }
    public void InputHandler(string data)
    {
        //Debug.Log(data);
        switch (data)
        {
            case "button1press":
                GameControl.Button1Count += 1;
                GameControl.Button1 = true;
                break;
            case "button2press":
                GameControl.Button2Count += 1;
                GameControl.Button2 = true;
                break;
            case "button3press":
                GameControl.Button3Count += 1;
                GameControl.Button3 = true;
                break;
            case "button4press":
                GameControl.Button4Count += 1;
                GameControl.Button4 = true;
                break;
            case "button5press":
                GameControl.Button5Count += 1;
                GameControl.Button5 = true;
                break;
        }

    }
}