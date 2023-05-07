using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public bool dialogueEnded = false;
    public int dialogueCount = 0;

    public TMP_Text dialogueText;
    public Animator animator;
    public Animator AMI_animator;

    private string sentence;
    public bool timerStarted;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        //once the sentence is fully typed out, start timer
        if (dialogueText.text == sentence && timerStarted == false)
        {
            //activates timer after words are typed out
            FindObjectOfType<DialogueTimer>().ActivateTimer();

            //AMI_animator.SetBool("isSpeaking", true);

            //this is only here to stop the timer from being activated every frame
            timerStarted = true;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        //Debug.Log("starting conversation with: " + dialogue.name);
        dialogueEnded = false;

        //gets rid of previous sentences
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            //add sentence to queue
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        AMI_animator.SetBool("isSpeaking", true);
        timerStarted = false;

        if (sentences.Count == 0)
        {
            EndDialogue();
            Debug.Log("Dialogue #: " + dialogueCount);
            return;
        }

        sentence = sentences.Dequeue();
        
        //if a typed sentence is already running, it stops and starts new sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        
        //toCharArray() converts string into char array
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        AMI_animator.SetBool("isSpeaking", false);
        Debug.Log("end of conversation");
        dialogueEnded = true;
        dialogueCount++;

    }
}
