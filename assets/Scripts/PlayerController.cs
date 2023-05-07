using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float MouseSensitivity = 10;
    [SerializeField] bool lockCursor = true;
    [SerializeField] float speed = 5;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.3f;
    [SerializeField] float jumpheight = 5.0f;
    [SerializeField] float gravity = 9.81f;

    float cameraPitch = 0.0f;
    float verticalVelocity = 0.0f;
    public Transform weapon;
    Vector3 newRotation;

    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        updateMouseLook();
        UpdateMovement();
    }

    void updateMouseLook()
    {
        //Mouse X gets left and right input while Mouse Y gets up and Down input
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * MouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        //Vector3.right = (1,0,0) this code basically says: rotate the camera around the x axis
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        //looking left and right Vector3.up = (0,1,0)
        transform.Rotate(Vector3.up * currentMouseDelta.x * MouseSensitivity);

        //for some reason this line of code makes the gun spawn behind you
        //!!nevermind I figured it out. Main camera was facing the wrong direction
        weapon.rotation = playerCamera.rotation;
    }


    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed;

        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpheight;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);

    }
}
