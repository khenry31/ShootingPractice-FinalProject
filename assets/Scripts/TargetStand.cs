using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetStand : MonoBehaviour
{
    public float health = 100;

    public bool isShot = false;
    public bool isActive = false;
    public float riseTimer = 5f;
    public bool riseTimerActive;
    private Quaternion targetRotation;
    public float speed = 10f;
    public float rotationValue = 180f;
    private Quaternion originalRotation;
    public Vector3 direction = new Vector3(0,-1,0);
    public bool testTargetDummy;
    private DialogueManager dm;

    // Start is called before the first frame update
    void Start()
    {
        dm = GameObject.FindObjectOfType<DialogueManager>();
        //targetRotation = Quaternion.AngleAxis(rotationValue, direction);
        targetRotation = Quaternion.Euler(new Vector3(180f, 0, 0));
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {


        if(health <= 0) 
        {
            //points cant be given anymore
            isShot = true;

            //if the first target is shot, open the door. Only done once.
            if (!GameManager.FirstTargetDown)
            {
                GameManager.FirstTargetDown = true;
                this.GetComponent<DialogueTrigger>().TriggerDialogue();

                //if the player shoots the target down before the dialogue can finish
                if(dm.dialogueCount < 2)
                {
                    dm.dialogueCount++;
                }
                //GameManager.openSecondDoor();

            }


            if(riseTimerActive)
            {
                riseTimer -= Time.deltaTime;
            }

            if (GameManager.TestEnded && !riseTimerActive && testTargetDummy)
            {
                riseTimerActive = true;
            }

            if (GameManager.TestStarted && riseTimerActive && testTargetDummy)
            {
                riseTimerActive = false;
            }
            

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, speed * Time.deltaTime);

            //wait a couple of seconds before it resets
            if (riseTimer <= 0)
            {
                riseTimer = 3f;
                health = 100;
                isShot = false;
            }
        }


        //only problem with this is that the player can shoot it before it can fully rise back up but i'm too tired to find out how to prevent this
        if (!isShot)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, originalRotation, speed * Time.deltaTime);
            
        }


    }

    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;
    }



}


