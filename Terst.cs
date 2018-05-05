using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Terst : MonoBehaviour
{
public SerialController serialController;
public GameObject cam;
public UnityEngine.Video.VideoPlayer videoPlayer;
void OnEnable()
{
								GameControl.Button1Count = 0;
								GameControl.Button2Count = 0;
								GameControl.Button3Count = 0;
								GameControl.Button4Count = 0;
								GameControl.Button5Count = 0;
								serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
}
private void Awake()
{
								// // Will attach a VideoPlayer to the main camera.
								cam = GameObject.Find("Main Camera");

								// // Add handler for loopPointReached
								// videoPlayer.Prepare();
								// videoPlayer.Play();
								videoPlayer = GameObject.Find("Main Camera").AddComponent<UnityEngine.Video.VideoPlayer>();
								videoPlayer.url = "Assets/Movies/" + GameControl.CurrentAnimal + "Ans.mp4";
								videoPlayer.playOnAwake = false;
								videoPlayer.isLooping = false;
								videoPlayer.loopPointReached += EndReached;
								videoPlayer.prepareCompleted += Prepared;
								videoPlayer.Prepare();
}
void Start()
{
}

private void Update()
{
								if (GameControl.Button1Count > 0 && GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
								{
																//send stop message
																serialController.SendSerialMessage("s");
																SceneManager.LoadScene("PressButton");
								}
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
}

void OnGUI(){
								if(Event.current.Equals(Event.KeyboardEvent("g")))
								{
																serialController.SendSerialMessage("b1");
								}
								else if(Event.current.Equals(Event.KeyboardEvent("a")))
								{
																serialController.SendSerialMessage("b2");
								}
								else if(Event.current.Equals(Event.KeyboardEvent("s")))
								{
																serialController.SendSerialMessage("b3");
								}
								else if(Event.current.Equals(Event.KeyboardEvent("d")))
								{
																serialController.SendSerialMessage("b4");
								}
								else if(Event.current.Equals(Event.KeyboardEvent("f")))
								{
																serialController.SendSerialMessage("b5");
								}
}
void Prepared(UnityEngine.Video.VideoPlayer vp){
								vp.Play();
}

void EndReached(UnityEngine.Video.VideoPlayer vp)
{
								SceneManager.LoadScene("PressButton");
}

public void InputHandler(string data)
{
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
