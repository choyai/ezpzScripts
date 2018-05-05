using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Randomizer : MonoBehaviour
{
public SerialController serialController;
public UnityEngine.Video.VideoPlayer videoPlayer;
public AudioSource audioSource;
void OnEnable()
{

}
// Use this for initialization
void Start()
{
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        GameControl.Button1Count = 0;
        GameControl.Button2Count = 0;
        GameControl.Button3Count = 0;
        GameControl.Button4Count = 0;
        GameControl.Button5Count = 0;
        RandomScene();
        serialController.SendSerialMessage("q");
        videoPlayer = GameObject.Find("Main Camera").AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        audioSource = GameObject.Find("PressButtonLoop" + "_1").GetComponent<AudioSource>();
        videoPlayer.url = "Assets/Movies/" + "PressButtonLoop" + ".mp4";

        videoPlayer.isLooping = true;
        // Add handler for loopPointReached
        videoPlayer.prepareCompleted += Prepared;
        videoPlayer.Prepare();
}

// Update is called once per frame
void Update()
{
        if (GameControl.Button1Count > 0 && GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0)
        {
                StartCoroutine(LoadNextScene(GameControl.CurrentAnimal + "Intro"));
        }
        Debug.Log(GameControl.Button1Count > 0 && GameControl.Button2Count > 0 && GameControl.Button3Count > 0 && GameControl.Button4Count > 0 && GameControl.Button5Count > 0);
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
                        asyncOperation.allowSceneActivation = true;
                }
                yield return null;
        }

}
void OnGUI(){
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
}
public string RandomScene()
{
        System.Random r = new System.Random();
        int index = r.Next(0, GameControl.Animals.Count);
        string Animal = GameControl.Animals[index];
        GameControl.Animals.RemoveAt(index);
        GameControl.CurrentAnimal = Animal;
        Debug.Log(GameControl.Animals.Count);
        return Animal;
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
void Prepared(UnityEngine.Video.VideoPlayer vp)
{
        vp.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        vp.Play();
        audioSource.Play();
        vp.loopPointReached -= Prepared;
}

void EndReached(UnityEngine.Video.VideoPlayer vp)
{
        SceneManager.LoadScene("PressButton");
}

}
