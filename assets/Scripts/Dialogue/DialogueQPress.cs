using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueQPress : MonoBehaviour
{
    //private bool dialogeStarted = false;
    DialogueManager dialogueManager;
    private float skipCount;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //change this to have it wait 5 seconds
        if (Input.GetKeyDown(KeyCode.Q) && !dialogueManager.dialogueEnded)
        {
            dialogueManager.DisplayNextSentence();
            skipCount++;
        }
    }
}
