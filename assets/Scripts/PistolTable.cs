using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PistolTable : MonoBehaviour
{
    private DialogueManager dialogueManager;
    [SerializeField] private float speed = 3f;
    private Vector3 newPosition;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        newPosition = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
        //originalPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueManager.dialogueCount == 1)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
        }


    }
}
