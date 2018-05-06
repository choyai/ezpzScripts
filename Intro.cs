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
        if(Event.current.Equals(Event.KeyboardEvent("g"))) {
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
        for (int i = 1; i < 10; i++) {
                if(Event.current.Equals(Event.KeyboardEvent(i.ToString()))) {
                        serialController.portName = "COM" + i.ToString();
                }
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
