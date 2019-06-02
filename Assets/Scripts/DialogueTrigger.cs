using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void Start()
    {
        FindObjectOfType<DialogueSystem>().StartDialogue(dialogue);
    }
}

