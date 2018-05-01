using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WriteToArduino("button1press");
        if (GameControl.Button1count > 0 && GameControl.Button2count > 0 && GameControl.Button3count > 0 && GameControl.Button4count > 0 && GameControl.Button5count > 0)
        {
            GameControl.Button1count = 0;
            GameControl.Button2count = 0;
            GameControl.Button3count = 0;
            GameControl.Button4count = 0;
            GameControl.Button5count = 0;
            SceneManager.LoadScene("Randomizer");
        }
    }
    public void WriteToArduino(string message)
    {
        GameControl.stream.WriteLine(message);
        GameControl.stream.BaseStream.Flush();
    }
}
