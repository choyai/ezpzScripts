using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class AnimalLoop : MonoBehaviour
{
public SerialController serialController;
public AudioSource audioSource;
public UnityEngine.Video.VideoPlayer videoPlayer;
public static string[] loopscenes = new string[]
{
        "IfYouKnow",
        "WhatIsIt"
};
public static List<string> LoopScenes = new List<string>(loopscenes);
public int index;
// Use this for initialization
void Awake()
{
        GameControl.Button1Count = 0;
        GameControl.Button2Count = 0;
        GameControl.Button3Count = 0;
        GameControl.Button4Count = 0;
        GameControl.Button5Count = 0;
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
}
//At start, start to load the next random lines of dialogue.
void Start(){
        System.Random rand = new System.Random();

        //send message to start receiving data
        serialController.SendSerialMessage("q");

        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;

        index = rand.Next(0, LoopScenes.Count);
        audioSource = GameObject.Find(GameControl.CurrentAnimal + LoopScenes[index] + "_1").GetComponent<AudioSource>();
        videoPlayer.url = "Assets/Movies/" + GameControl.CurrentAnimal + LoopScenes[index] + ".mp4";
        Debug.Log("Fetching" + LoopScenes[index]);
        videoPlayer.isLooping = false;
        // Add handler for loopPointReached
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.prepareCompleted += Prepared;
        videoPlayer.Prepare();
}

// Update is called once per frame
void Update()
{

        if (GameControl.Button1Count > 0 && GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
        {
                //send stop message
                serialController.SendSerialMessage("s");
                SceneManager.LoadScene("Randomizer");
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

public IEnumerator LoadNextScene(string sceneName)
{
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
                if (asyncOperation.progress >= 0.9f)
                {
                        System.Random r = new System.Random();
                        int seconds = r.Next(3, 7);
                        yield return new WaitForSeconds(seconds);
                        asyncOperation.allowSceneActivation = true;
                }
                yield return null;
        }

}
public IEnumerator LoadShortClip(string vidName){
        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.url = "Assets/Movies/" + GameControl.CurrentAnimal + "IfYouKnow.mp4";
        videoPlayer.isLooping = false;
        // Add handler for loopPointReached
        videoPlayer.Prepare();
        videoPlayer.Play();
        videoPlayer = GameObject.Find(GameControl.CurrentAnimal + "IfYouKnow").GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
        yield return null;
}



private void OnGUI()
{
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

public void InputHandler(string data)
{
        Debug.Log("yo");
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

async void Prepared(UnityEngine.Video.VideoPlayer vp){
        System.Random r = new System.Random();
        Debug.Log("prepared");
        LoopScenes.RemoveAt(index);
        if(LoopScenes.Count < 1) {
                LoopScenes.AddRange(loopscenes);
        }
        index = r.Next(0, LoopScenes.Count);
        int seconds = r.Next(3, 7);
        Debug.Log("gimme" + seconds.ToString());
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        vp.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        Debug.Log("Done?");
        vp.Play();
        audioSource.Play();

}

void EndReached(UnityEngine.Video.VideoPlayer vp){
        audioSource =  GameObject.Find(GameControl.CurrentAnimal + LoopScenes[index] + "_1").GetComponent<AudioSource>();
        vp.renderMode = UnityEngine.Video.VideoRenderMode.CameraFarPlane;
        vp.playOnAwake = false;
        vp.url = "Assets/Movies/" + GameControl.CurrentAnimal + LoopScenes[index] + ".mp4";
        Debug.Log("Fetching" + LoopScenes[index]);
        vp.isLooping = false;
        vp.Prepare();
}

}
