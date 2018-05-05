using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
public SerialController serialController;
public UnityEngine.Video.VideoPlayer videoPlayer;
// Use this for initialization
void OnEnable()
{
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
}
void Start(){
        serialController.SendSerialMessage("q");
        videoPlayer = GameObject.Find("Main Camera").GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
}

void OnGUI(){
        if(Event.current.Equals(Event.KeyboardEvent("return"))) {
                serialController.SendSerialMessage("b1");
        }
        else if(Event.current.Equals(Event.KeyboardEvent("a"))) {
                serialController.SendSerialMessage("b2");
        }
        else if(Event.current.Equals(Event.KeyboardEvent("s"))) {
                serialController.SendSerialMessage("b3");
        }
        else if(Event.current.Equals(Event.KeyboardEvent("d"))) {
                serialController.SendSerialMessage("b4");
        }
        else if(Event.current.Equals(Event.KeyboardEvent("f"))) {
                serialController.SendSerialMessage("b5");
        }
}

// Update is called once per frame
void Update()
{
        if (GameControl.Button1Count > 0 && GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
        {
                serialController.SendSerialMessage("s");
                SceneManager.LoadScene("Randomizer");
        }
}

void EndReached(UnityEngine.Video.VideoPlayer vp)
{
        SceneManager.LoadScene("Randomizer");
}

}
