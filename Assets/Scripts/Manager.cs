using System.Collections.Generic;
using UnityEngine;


/* global vars */
public enum GameProgress { HowToPlay, GameOver }

public class Manager
{
    // The singleton instance of the Manager class
    private static Manager instance;

    // Any global variables or events can be defined here
    public Dictionary<GameProgress, bool> ProgressDict { get; set; }

    /* Messages*/

    public Dictionary<GameProgress, string[]> MessageDict = new Dictionary<GameProgress, string[]>
    {
        { GameProgress.HowToPlay, new string[] { "Press Space to Jump", "Move Platforms with L and R Keys" } },
        {GameProgress.GameOver,  new string[]{ "Game Over"  } }
    };

    public string[] GetProgressMessage(GameProgress key) {
        return MessageDict[key];
    }


    // Get the singleton instance of the Manager class
    public static Manager GetInstance()
    {
        if (instance == null)
        {
            instance = new Manager();
        }
        return instance;
    }

    public bool GetProgressByKey(GameProgress key)
    {
        return ProgressDict[key];
    }

}
