using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDialogueTrigger : MonoBehaviour
{
    bool activated = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!activated)
        {
            this.GetComponent<DialogueTrigger>().TriggerDialogue();
            activated = true;
        }
        
    }
}
