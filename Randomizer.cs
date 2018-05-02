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
        StartCoroutine(LoadNextScene(GameControl.CurrentAnimal + "Intro"));
    }

    public IEnumerator LoadNextScene(string sceneName)
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.99f)
            {
                asyncOperation.allowSceneActivation = true;
            }
        }
        yield return null;
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
}
