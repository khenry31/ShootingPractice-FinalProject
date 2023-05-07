using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startDialogue : MonoBehaviour
{

    public float timer = 3f;
    private bool timerComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerComplete)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }

            if (timer <= 0f)
            {
                this.GetComponent<DialogueTrigger>().TriggerDialogue();
                timerComplete = true;
            }
        }
    }
}
