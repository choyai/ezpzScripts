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
}
void Start(){
								serialController.SendSerialMessage("q");
								videoPlayer = GameObject.Find("Main Camera").AddComponent<UnityEngine.Video.VideoPlayer>();
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
								if (GameControl.Button1Count > 0 && GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
								{
																serialController.SendSerialMessage("s");
																SceneManager.LoadScene("Randomizer");
								}
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

}
