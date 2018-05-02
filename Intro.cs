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

    }
    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
            SceneManager.LoadScene("Randomizer");
        }
    }
    public void WriteToArduino(string message)
    {
        GameControl.stream.WriteLine(message);
        GameControl.stream.BaseStream.Flush();
    }
}
