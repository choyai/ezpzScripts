using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressButton1 : MonoBehaviour
{
public SerialController serialController;
public UnityEngine.Video.VideoPlayer videoPlayer;
public AudioSource audioSource;
// Use this for initialization
void OnEnable()
{
								GameControl.Button1Count = 0;
								GameControl.Button2Count = 0;
								GameControl.Button3Count = 0;
								GameControl.Button4Count = 0;
								GameControl.Button5Count = 0;
								serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
								GameObject oldCam = GameObject.Find("Main Camera");
								Destroy(oldCam);
}
void Start(){
								serialController.SendSerialMessage("2");
								videoPlayer = GameObject.Find("MaiCamera").AddComponent<UnityEngine.Video.VideoPlayer>();
								videoPlayer.playOnAwake = false;

								videoPlayer.url = "Assets/Movies/" + "PressButton" + ".mp4";

								videoPlayer.isLooping = false;
								// Add handler for loopPointReached
								videoPlayer.loopPointReached += EndReached;
								videoPlayer.prepareCompleted += Prepared;
								videoPlayer.Prepare();
}

void OnGUI(){

}

// Update is called once per frame
void Update()
{
								string message = serialController.ReadSerialMessage();


								if (message == null)
																return;

								// Check if the message is plain data or a connect/disconnect event.
								if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
																Debug.Log("Connection established");
								else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
																Debug.Log("Connection attempt failed or disconnection detected");
								else
																Debug.Log("Message arrived: " + message);
								InputHandler(message);
								// if (GameControl.Button1Count > 0 && GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
								// {
								//         // serialController.SendSerialMessage("s");
								//         SceneManager.LoadScene("Randomizer");
								// }
}
void Prepared(UnityEngine.Video.VideoPlayer vp)
{
								vp.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
								vp.Play();

}

void EndReached(UnityEngine.Video.VideoPlayer vp)
{
								SceneManager.LoadScene("Randomizer");
}

void InputHandler(string message){
								return;
}
}
