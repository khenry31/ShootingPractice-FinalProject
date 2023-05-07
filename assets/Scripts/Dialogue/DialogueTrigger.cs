using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        //GameManager.DialogueCount++;
        //Debug.Log("Dialogue #: " + GameManager.DialogueCount);
        //start timer
        //FindObjectOfType<DialogueTimer>().ActivateTimer();
    }
}
