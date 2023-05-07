using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class lowerDoor : MonoBehaviour
{
    Weapon weaponScript;
    [SerializeField] private float speed = 5f;
    private Vector3 newPosition;
    private bool dialogueStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        weaponScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
        newPosition = new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponScript.hasSecondary)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
        }

        //only play dialogue once
        if(weaponScript.hasSecondary && !dialogueStarted)
        {
            dialogueStarted = true;
            this.GetComponent<DialogueTrigger>().TriggerDialogue();
        }

    }
}
