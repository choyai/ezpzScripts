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
public static string[] loopscenes = new string[]
{
        "IfYouKnow",
        "WhatIsIt"
};
public static List<string> LoopScenes = new List<string>(loopscenes);
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
//At start, start to load the next random lines of dialogue.
void Start(){
        //send message to start receiving data
        serialController.SendSerialMessage("q");
        System.Random rand = new System.Random();
        int index = rand.Next(0, LoopScenes.Count);
        StartCoroutine(LoadNextScene(GameControl.CurrentAnimal + LoopScenes[index]));
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
        yield return null;
}

private void OnGUI()
{
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
}
