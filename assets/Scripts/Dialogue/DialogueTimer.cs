using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTimer : MonoBehaviour
{
    private bool timerActivated = false;
    public float timer = 3f;
    public float timerAmount;
    private DialogueManager dialogueManager;
    public Animator AMI_animator;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        timerAmount = timer;
    }

    // Update is called once per frame
    void Update()
    {
        //once the first dialogue starts, display next sentence after 5 seconds pass.
        if (timerActivated && !dialogueManager.dialogueEnded)
        {

            //once dialogue has started, start timer
            if (timer >= 0f)
            {
                AMI_animator.SetBool("isSpeaking", false);
                timer -= Time.deltaTime;
                Debug.Log("Timer: " + timer);
            }

            //after timer ends, display next sentence and restart timer
            if (timer <= 0f)
            {
                dialogueManager.DisplayNextSentence();
                timer = timerAmount;

                if (!dialogueManager.dialogueEnded)
                {
                    AMI_animator.SetBool("isSpeaking", true);
                }
                
                //deactivates timer once timer runs out
                timerActivated = false;
            }
        }

        else
        {
            timerActivated = false;
        }
       
    }

    public void ActivateTimer()
    {
        timerActivated = true;
    }

    public void deactivateTimer()
    {
        timerActivated = false;
    }

}
