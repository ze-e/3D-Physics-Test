using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Settings;
public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static Manager Instance { get { return instance; } }
    public Player player;
    private Dictionary<GameProgress, bool> ProgressDict  = new Dictionary<GameProgress, bool>();

    /* UI */
    public TMP_Text messageUI;
    private Coroutine displayMessageCoroutine;

    #region start

    private void Awake()
    {
        InitManager();

        // initialize data
        InitData();

        // subscribe to events
        InitEvents();

        // show intro text (how to play)
        DisplayMessage(GameProgress.HowToPlay, 5f);
    }

    void InitManager()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitData()
    {
        CreateGameProgressDict();
    }

    void InitEvents()
    {
        player.DeathEvent += DeathHandler;
    }


    #endregion start


    #region messages

    public string[] GetProgressMessage(GameProgress key)
    {
        MessageData[] _messages = Resources.FindObjectsOfTypeAll<MessageData>();
        foreach (MessageData _message in _messages)
        {
            if (_message.MessageName == key)
            {
                return _message.Message;
            }
        }

        return null;
    }

    //public string[] GetProgressMessage(GameProgress key)
    //{
    //    return Messages.MessageDict[key];
    //}

    public void DisplayMessage(GameProgress key, float duration)
    {
        if (displayMessageCoroutine != null)
        {
            StopCoroutine(displayMessageCoroutine);
        }

        string[] _text = GetProgressMessage(key);
        if (_text == null || _text.Length == 0) Debug.LogWarning("Message for " + key + " not loaded");
        else displayMessageCoroutine = StartCoroutine(DisplayMessageCoroutine(_text, key, duration));
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


    #endregion messages

    #region progress

     void CreateGameProgressDict()
    {
        foreach (GameProgress progress in System.Enum.GetValues(typeof(GameProgress)))
        {
            ProgressDict.Add(progress, false);
        }
    }

    public bool GetProgressByKey(GameProgress key)
    {
        return ProgressDict[key];
    }

    public void SetProgressByKey(GameProgress key, bool val)
    {
        ProgressDict[key] = val;
    }

    #endregion progress

    #region events

    /* Death */
    public void DeathHandler()
    {
        SetProgressByKey(GameProgress.GameOver, true);
        DisplayMessage(GameProgress.GameOver, 3f);
        Invoke(nameof(ReloadLevel), 3f);
    }
    private void ReloadLevel()
    {
        SetProgressByKey(GameProgress.GameOver, false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    #endregion events
}
