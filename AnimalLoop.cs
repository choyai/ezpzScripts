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
        StartCoroutine
        (
                CoReadFromArduino
                        ((s) => InputHandler(s), // Callback
                        () => Debug.LogError("Error!"), // Error callback
                        10000f                      // Timeout (milliseconds)
                        )
        );
        //Debug.Log(message: GameControl.Button1Count);
        if (GameControl.Button1Count > 0 && GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
        {
                SceneManager.LoadScene("Randomizer");
        }
}
private void OnGUI()
{
        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
                // Debug.Log("please");
                //WriteToArduino("button1press");
                //WriteToArduino("button2press");
                //WriteToArduino("button3press");
                //WriteToArduino("button4press");
                //WriteToArduino("button5press");
                // if (GameControl.Button1Count > 0) /*&& GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)*/
                // {
                SceneManager.LoadScene("Randomizer");
                // }
        }
}
public IEnumerator CoReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
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
                        Debug.Log(dataString);
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
                        yield return new WaitForSeconds(0.1f);

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
