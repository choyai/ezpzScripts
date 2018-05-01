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
        SceneManager.LoadScene(GameControl.CurrentAnimal + "Intro");
    }

    public string RandomScene()
    {
        System.Random r = new System.Random();
        int index = r.Next(0, GameControl.Animals.Count);
        string Animal = GameControl.Animals[index];
        GameControl.Animals.RemoveAt(index);
        GameControl.CurrentAnimal = Animal;
        Debug.Log(Animal + " " + GameControl.Animals.Count);
        return Animal;
    }
}
