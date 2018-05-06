using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalIntro : MonoBehaviour
{
public SerialController serialController;
private void Awake()
{
        GameControl.Button1Count = 0;
        GameControl.Button2Count = 0;
        GameControl.Button3Count = 0;
        GameControl.Button4Count = 0;
        GameControl.Button5Count = 0;
}

public UnityEngine.Video.VideoPlayer videoPlayer;


void Start()
{
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();

        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();

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
        videoPlayer.url = "Assets/Movies/" + GameControl.CurrentAnimal + "Intro.mp4";

        // Skip the first 100 frames.
        //videoPlayer.frame = 100;

        // Restart from beginning when done.
        videoPlayer.isLooping = false;

        // Add handler for loopPointReached
        videoPlayer.loopPointReached += EndReached;

        // Start playback. This means the VideoPlayer may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
        switch(GameControl.CurrentAnimal) {

        case "Elephant":
                serialController.SendSerialMessage("B");

                break;
        case "Lion":
                serialController.SendSerialMessage("R");

                break;
        default:
                serialController.SendSerialMessage("G");
                break;
        }
        videoPlayer.Play();

}

private void Update()
{
        string message = serialController.ReadSerialMessage();
        switch(GameControl.CurrentAnimal) {
        case "Bear":

                if(videoPlayer.time == 10f) {
                        serialController.SendSerialMessage("V");
                        serialController.SendSerialMessage("F");
                }
                if(videoPlayer.time == 11f) {
                        serialController.SendSerialMessage("v");
                        serialController.SendSerialMessage("f");
                }
                break;
        case "Elephant":

                if(videoPlayer.time == 8.5f) {
                        serialController.SendSerialMessage("V");
                        serialController.SendSerialMessage("f");
                }
                if(videoPlayer.time == 9f) {
                        serialController.SendSerialMessage("v");
                        serialController.SendSerialMessage("f");
                }
                break;
        case "Lion":

                if(videoPlayer.time == 2f) {
                        serialController.SendSerialMessage("V");
                        serialController.SendSerialMessage("F");
                }
                if(videoPlayer.time == 3f) {
                        serialController.SendSerialMessage("v");
                        serialController.SendSerialMessage("f");
                }
                break;

        }

}



void EndReached(UnityEngine.Video.VideoPlayer vp)
{

        SceneManager.LoadScene(GameControl.CurrentAnimal + "Loop");
}
}
