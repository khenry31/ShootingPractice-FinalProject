using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 5f;
    private Vector3 newPosition;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        newPosition = new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.TestEnded)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, speed * Time.deltaTime);
        }
    }
}
