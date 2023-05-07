using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueInvisibleTrigger : MonoBehaviour
{
    private bool dialogeStarted = false;
    private bool playerCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //change this to have it wait 5 seconds
        if (playerCollided && !dialogeStarted)
        {
            dialogeStarted = true;

            //triggers dialogue after pressing Q.
            this.GetComponent<DialogueTrigger>().TriggerDialogue();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerCollided = true;
    }
}
