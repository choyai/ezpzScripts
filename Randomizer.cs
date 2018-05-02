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
        // StartCoroutine
        //(
        //        AsynchronousReadFromArduino
        //                ((s) => InputHandler(s), // Callback
        //                () => Debug.LogError("Error!"), // Error callback
        //                10000f                      // Timeout (milliseconds)
        //                )
        //);
        StartCoroutine(LoadNextScene(GameControl.CurrentAnimal + "Intro"));

    }
    public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        string dataString = null;

        do
        {
            try
            {
                dataString = GameControl.stream.ReadLine();
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                callback(dataString);
                yield return null;
            }
            else
                yield return new WaitForSeconds(0.05f);

            nowTime = DateTime.Now;
            diff = nowTime - initialTime;

        } while (diff.Milliseconds < timeout);

        if (fail != null)
            fail();
        yield return null;
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
