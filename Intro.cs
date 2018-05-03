using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
public SerialController serialController;

// Use this for initialization
void OnEnable()
{
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
}

// Update is called once per frame
void Update()
{
        if (GameControl.Button1Count > 0) //&& GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
        {
                SceneManager.LoadScene("Randomizer");
        }
}
private void OnGUI()
{
        if (Event.current.Equals(Event.KeyboardEvent("return")))
        {
                // SceneManager.LoadScene("Randomizer");
                serialController.SendSerialMessage("b1");
        }


}
// public void WriteToArduino(string message)
// {
//         GameControl.stream.WriteLine(message);
//         GameControl.stream.BaseStream.Flush();
// }
}
