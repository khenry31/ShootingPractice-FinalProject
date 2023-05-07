using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class endTest : MonoBehaviour
{
    Dialogue dialogue;

    private void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {

        if (!GameManager.TestEnded)
        {
            this.GetComponent<DialogueTrigger>().TriggerDialogue();
            GameManager.TestEnded = true;
            GameManager.TestStarted = false;
        }
    }

    
}
