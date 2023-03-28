using System;
using System.Collections.Generic;
using UnityEngine;


/* global vars */
public enum GameProgress { HowToPlay, GameOver }

public class Manager : MonoBehaviour
{
    // The singleton instance of the Manager class
    private static Manager instance;

    // Any global variables or events can be defined here
    public Dictionary<GameProgress, bool> ProgressDict = new Dictionary<GameProgress, bool>   
    {
        { GameProgress.HowToPlay, false },
        {GameProgress.GameOver,  false }
    };

    // Get the singleton instance of the Manager class
    public static Manager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Manager>();
            if (instance == null)
            {
                GameObject managerObject = new GameObject("Manager");
                instance = managerObject.AddComponent<Manager>();
                DontDestroyOnLoad(managerObject);
            }
        }
        return instance;
    }

    private void Start()
    {
        GetInstance();
    }

    /* Messages*/

    public Dictionary<GameProgress, string[]> MessageDict = new Dictionary<GameProgress, string[]>
    {
        { GameProgress.HowToPlay, new string[] { "Press Space to Jump", "Move Platforms with L and R Arrow Keys", "While falling, you can use L and R to direct your fall" } },
        {GameProgress.GameOver,  new string[]{ "Game Over"  } }
    };

    public string[] GetProgressMessage(GameProgress key) {
        return MessageDict[key];
    }

   

    public bool GetProgressByKey(GameProgress key)
    {
        return ProgressDict[key];
    }

    public void SetProgressByKey(GameProgress key, bool val)
    {
        ProgressDict[key] = val;
    }

    /* Displaying Text */

    public void DisplayMessage(GameProgress key, float duration)
    {
        CameraFollow _camera = Camera.main.GetComponent<CameraFollow>();
        _camera.DisplayMessage(key, duration);  
    }
}
