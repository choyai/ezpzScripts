using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Randomizer : MonoBehaviour
{

// Use this for initialization
void Start()
{
        RandomScene();
}

// Update is called once per frame
void Update()
{
        // if(GameControl.Animals.Count > 4) {
        StartCoroutine(LoadNextScene(GameControl.CurrentAnimal + "Intro"));
        // }
        // else
        // {
        // StartCoroutine(LoadNextScene())
        // }
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
        Debug.Log(data);
        switch (data)
        {
        case "button1press":
                GameControl.Button1Count += 1;
                GameControl.Button1 = true;
                break;
        case "button2press":
                GameControl.Button2Count += 1;
                GameControl.Button2 = true;
                break;
        case "button3press":
                GameControl.Button3Count += 1;
                GameControl.Button3 = true;
                break;
        case "button4press":
                GameControl.Button4Count += 1;
                GameControl.Button4 = true;
                break;
        case "button5press":
                GameControl.Button5Count += 1;
                GameControl.Button5 = true;
                break;
        }

}
}
