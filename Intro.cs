using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
public SerialController serialController;
public UnityEngine.Video.VideoPlayer videoPlayer;
public AudioSource audioSource;
// Use this for initialization
void OnEnable()
{
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
}
void Start(){
        serialController.SendSerialMessage("q");
        videoPlayer = GameObject.Find("Main Camera").AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        audioSource = GameObject.Find("op" + "_1").GetComponent<AudioSource>();
        videoPlayer.url = "Assets/Movies/" + "op" + ".mp4";

        videoPlayer.isLooping = false;
        // Add handler for loopPointReached
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.prepareCompleted += Prepared;
        videoPlayer.Prepare();
}

void OnGUI(){
        GUILayout.Label("Type COM Port number to set COM Port");
        if(Event.current.Equals(Event.KeyboardEvent("1"))) {
                serialController.portName = "COM1";
        }
        else if(Event.current.Equals(Event.KeyboardEvent("2"))) {
                serialController.portName = "COM2";
        }
        else if(Event.current.Equals(Event.KeyboardEvent("3"))) {
                serialController.portName = "COM3";
        }
        else if(Event.current.Equals(Event.KeyboardEvent("4"))) {
                serialController.portName = "COM4";
        }
        else if(Event.current.Equals(Event.KeyboardEvent("5"))) {
                serialController.portName = "COM5";
        }
        else if(Event.current.Equals(Event.KeyboardEvent("6"))) {
                serialController.portName = "COM6";
        }
        else if(Event.current.Equals(Event.KeyboardEvent("7"))) {
                serialController.portName = "COM7";
        }
        else if(Event.current.Equals(Event.KeyboardEvent("8"))) {
                serialController.portName = "COM8";
        }
        else if(Event.current.Equals(Event.KeyboardEvent("9"))) {
                serialController.portName = "COM9";
        }
        else if(Event.current.Equals(Event.KeyboardEvent("return"))) {
                SceneManager.LoadScene("HowToPlay");
        }
}

// Update is called once per frame
void Update()
{
        if (GameControl.Button1Count > 0 && GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
        {
                // serialController.SendSerialMessage("s");
                SceneManager.LoadScene("HowToPlay");
        }
}

void Prepared(UnityEngine.Video.VideoPlayer vp)
{
        vp.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        vp.Play();
        audioSource.Play();
}

void EndReached(UnityEngine.Video.VideoPlayer vp)
{
        SceneManager.LoadScene("HowToPlay");
}

}
