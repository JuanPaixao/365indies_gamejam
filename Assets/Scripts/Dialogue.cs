using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueList")]
public class Dialogue : ScriptableObject
{
    public string NPCName;
    public string[] sentences;
}
