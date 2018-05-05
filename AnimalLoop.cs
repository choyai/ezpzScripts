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
public bool answered;
public bool correct;
public UnityEngine.Video.VideoPlayer videoPlayer;
public static string[] minigames = new string[] {
        // "Monkey",
        // "Rhino"
};
public static List<string> minigameslist = new List<string>(minigames);

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
        answered = false;
        correct = false;
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
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;

        index = rand.Next(0, LoopScenes.Count);
        audioSource = GameObject.Find(GameControl.CurrentAnimal + LoopScenes[index] + "_1").GetComponent<AudioSource>();
        videoPlayer.url = "Assets/Movies/" + GameControl.CurrentAnimal + LoopScenes[index] + ".mp4";
        Debug.Log("Fetching" + LoopScenes[index]);
        videoPlayer.isLooping = false;
        // Add handler for loopPointReached
        videoPlayer.loopPointReached += LoopClipEndReached;
        videoPlayer.prepareCompleted += Prepared;
        videoPlayer.Prepare();
}

// Update is called once per frame
void Update()
{
//This one checks the state of the ofdjfoe
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
        else if(Event.current.Equals(Event.KeyboardEvent("c"))) {
                serialController.SendSerialMessage(GameControl.CurrentAnimal);
        }
        else if(Event.current.Equals(Event.KeyboardEvent("i"))) {
                serialController.SendSerialMessage("wrong");
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
        if(data == GameControl.CurrentAnimal && !answered) {
                correct = true;
                answered = true;
                if(videoPlayer.isPlaying) {
                        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraFarPlane;
                        videoPlayer.Stop();
                        audioSource.Stop();
                        videoPlayer.url = "Assets/Movies/" + GameControl.CurrentAnimal + "Correct" + ".mp4";
                        audioSource = GameObject.Find(GameControl.CurrentAnimal + "Correct").GetComponent<AudioSource>();
                        videoPlayer.prepareCompleted -= LoopClipEndReached;
                        videoPlayer.prepareCompleted += PreparedAns;
                        videoPlayer.loopPointReached += ansEndReached;
                        videoPlayer.Prepare();
                }
                else{
                        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraFarPlane;
                        videoPlayer.url = "Assets/Movies/" + GameControl.CurrentAnimal + "Correct" + ".mp4";
                        audioSource = GameObject.Find(GameControl.CurrentAnimal + "Correct").GetComponent<AudioSource>();
                        videoPlayer.prepareCompleted -= Prepared;
                        videoPlayer.prepareCompleted += PreparedAns;
                        videoPlayer.loopPointReached += ansEndReached;
                        videoPlayer.Prepare();
                }
                serialController.SendSerialMessage("s");
        }
        else if (data == "wrong" && !answered) {
                correct = false;
                answered = true;
                if(videoPlayer.isPlaying) {
                        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraFarPlane;
                        videoPlayer.prepareCompleted -= LoopClipEndReached;
                        videoPlayer.Stop();
                        audioSource.Stop();
                        videoPlayer.url = "Assets/Movies/" + "Wrong" + GameControl.CurrentAnimal + ".mp4";
                        audioSource = GameObject.Find("Wrong" + GameControl.CurrentAnimal).GetComponent<AudioSource>();
                        videoPlayer.prepareCompleted += PreparedAns;
                        videoPlayer.loopPointReached += ansEndReached;
                        videoPlayer.Prepare();
                }
                else{
                        videoPlayer.url = "Assets/Movies/" + "Wrong" + GameControl.CurrentAnimal + ".mp4";
                        audioSource = GameObject.Find("Wrong" + GameControl.CurrentAnimal).GetComponent<AudioSource>();
                        videoPlayer.prepareCompleted -= Prepared;
                        videoPlayer.prepareCompleted += PreparedAns;
                        videoPlayer.loopPointReached += ansEndReached;
                        videoPlayer.Prepare();
                }
                serialController.SendSerialMessage("s");
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
        if(!audioSource.isPlaying) {
                audioSource.Play();
        }
        vp.loopPointReached -= Prepared;
}

async void PreparedAns(UnityEngine.Video.VideoPlayer vp){
        vp.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        vp.Play();
        audioSource.Play();
        videoPlayer.prepareCompleted -= PreparedAns;
}

void LoopClipEndReached(UnityEngine.Video.VideoPlayer vp){
        audioSource =  GameObject.Find(GameControl.CurrentAnimal + LoopScenes[index] + "_1").GetComponent<AudioSource>();
        vp.renderMode = UnityEngine.Video.VideoRenderMode.CameraFarPlane;
        vp.playOnAwake = false;
        vp.url = "Assets/Movies/" + GameControl.CurrentAnimal + LoopScenes[index] + ".mp4";
        Debug.Log("Fetching" + LoopScenes[index]);
        vp.isLooping = false;
        vp.loopPointReached += Prepared;
        vp.Prepare();
}

void ansEndReached(UnityEngine.Video.VideoPlayer vp){
        if(correct) {
                videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraFarPlane;
                videoPlayer.Stop();
                audioSource.Stop();
                videoPlayer.url = "Assets/Movies/" + GameControl.CurrentAnimal + "Ans" + ".mp4";
                audioSource = GameObject.Find(GameControl.CurrentAnimal + "Ans" + "_1").GetComponent<AudioSource>();
                videoPlayer.Prepare();
                videoPlayer.prepareCompleted += PreparedAns;
                videoPlayer.loopPointReached += RandomAgain;
        }
        else{
                answered = false;
                audioSource =  GameObject.Find(GameControl.CurrentAnimal + LoopScenes[index] + "_1").GetComponent<AudioSource>();
                vp.renderMode = UnityEngine.Video.VideoRenderMode.CameraFarPlane;
                vp.playOnAwake = false;
                vp.url = "Assets/Movies/" + GameControl.CurrentAnimal + LoopScenes[index] + ".mp4";
                Debug.Log("Fetching" + LoopScenes[index]);
                vp.isLooping = false;
                vp.prepareCompleted += Prepared;
                vp.loopPointReached += LoopClipEndReached;
                vp.Prepare();
        }
}

void RandomAgain(UnityEngine.Video.VideoPlayer vp){
        if(minigameslist.Contains(GameControl.CurrentAnimal))
        {
                SceneManager.LoadScene(GameControl.CurrentAnimal + "Mini");
        }
        else{
                SceneManager.LoadScene("Randomizer");
        }

}
}
