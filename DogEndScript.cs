using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogEndScript : MonoBehaviour
{

public UnityEngine.Video.VideoPlayer videoPlayer;
public AudioSource audioSource;

private void Awake()
{
								GameControl.Button1Count = 0;
								GameControl.Button2Count = 0;
								GameControl.Button3Count = 0;
								GameControl.Button4Count = 0;
								GameControl.Button5Count = 0;

}
void Start()
{
								// Will attach a VideoPlayer to the main camera.
								GameObject camera = GameObject.Find("Main Camera");
								var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
								audioSource = GameObject.Find("EasyEnding" + "_1").GetComponent<AudioSource>();
								// Play on awake defaults to true. Set it to false to avoid the url set
								// below to auto-start playback since we're in Start().
								videoPlayer.playOnAwake = false;

								// By default, VideoPlayers added to a camera will use the far plane.
								// Let's target the near plane instead.
								//videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

								// This will cause our scene to be visible through the video being played.
								//videoPlayer.targetCameraAlpha = 0.5F;

								// Set the video to play. URL supports local absolute or relative paths.
								// Here, using absolute.
								videoPlayer.url = "Assets/Movies/" + "EasyEnding.mp4";

								// Skip the first 100 frames.
								//videoPlayer.frame = 100;

								// Restart from beginning when done.
								videoPlayer.isLooping = false;
								videoPlayer.prepareCompleted += Prepared;

								videoPlayer.Prepare();
}

private void Update()
{

}



void Prepared(UnityEngine.Video.VideoPlayer vp)
{
								vp.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
								vp.Play();
								audioSource.Play();
}
}
