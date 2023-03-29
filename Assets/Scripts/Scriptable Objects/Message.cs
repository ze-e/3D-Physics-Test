using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settings;

[CreateAssetMenu(fileName = "New MessageData", menuName = "Message")]
public class MessageData : ScriptableObject
{
    [HideInInspector]
    public GameProgress Key;
    public string[] Message;
}
