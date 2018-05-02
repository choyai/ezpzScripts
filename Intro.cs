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
        if (GameControl.buttons[0] == true && GameControl.buttons[1] == true && GameControl.buttons[2] == true && GameControl.buttons[3] == true && GameControl.buttons[4] == true)
        {
            SceneManager.LoadScene("Randomizer");
        }
    }
    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
            Debug.Log("WHY");
            SceneManager.LoadScene("Randomizer");
            WriteToArduino("b1p");
            WriteToArduino("b2p");
            WriteToArduino("b3p");
            WriteToArduino("b4p");
            WriteToArduino("b5p");

        }

    }
    public void WriteToArduino(string message)
    {
        GameControl.stream.WriteLine(message);
        GameControl.stream.BaseStream.Flush();
    }
}
