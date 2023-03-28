using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Settings;
public class Manager : MonoBehaviour
{
    private static Manager instance;
    public Player player;

    /* UI */
    public TMP_Text messageUI;
    private Coroutine displayMessageCoroutine;

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
        player.DeathEvent += DeathHandler;

        if (!GetProgressByKey(GameProgress.HowToPlay) )
        {
            DisplayMessage(GameProgress.HowToPlay, 10f);
        }

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

    /* Progress */
    public Dictionary<GameProgress, bool> ProgressDict = new Dictionary<GameProgress, bool>
    {
        { GameProgress.HowToPlay, false },
        { GameProgress.GameOver,  false }
    };

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
        if (displayMessageCoroutine != null)
        {
            StopCoroutine(displayMessageCoroutine);
        }

        string[] _text = GetProgressMessage(key);
        if (_text.Length == 0)
        {
            return;
        }
        displayMessageCoroutine = StartCoroutine(DisplayMessageCoroutine(_text, key, duration));
    }

    IEnumerator DisplayMessageCoroutine(string[] text, GameProgress key, float duration)
    {
        for (int i = 0; i < text.Length; i++)
        {
            messageUI.text = text[i];
            yield return new WaitForSeconds(duration);
        }

        messageUI.text = "";
        SetProgressByKey(key, true);
    }

    /* Death */
    public void DeathHandler()
    {
        SetProgressByKey(GameProgress.GameOver, true);
        DisplayMessage(GameProgress.GameOver, 3f);
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        SetProgressByKey(GameProgress.GameOver, false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
