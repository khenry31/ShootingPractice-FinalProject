using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowerSecondDoor : MonoBehaviour
{
    DialogueManager dialogueManager;
    [SerializeField] private float speed = 5f;
    private Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
        newPosition = new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager.dialogueCount == 3)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
        }
    }
}
