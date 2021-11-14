using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed, gravityModifire, jumpSpeed;
    public CharacterController characterController;
    public Transform cameraTransform;

    public bool invertedMouseX;
    public bool invertedMouseY;

    private float directionY;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveInput;
    private Vector2 mouseInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float storedY = moveInput.y;

        Vector3 horizontalMove = transform.right * Input.GetAxis("Horizontal");
        Vector3 verticalMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        if (Input.GetButtonDown("Jump"))
        {
            directionY = jumpSpeed;
        }

        directionY = gravityModifire;
        direction.y = directionY;

        moveInput = verticalMove + horizontalMove;
        moveInput.Normalize();
        moveInput = moveInput * moveSpeed;

        moveInput.y = storedY;

        moveInput.y += Physics.gravity.y * gravityModifire * Time.deltaTime;

        if (characterController.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifire * Time.deltaTime;
        }

        characterController.Move(moveInput * Time.deltaTime);

        mouseInput = new Vector2(x:Input.GetAxisRaw("Mouse X"), y:Input.GetAxisRaw("Mouse Y"));

        if (invertedMouseX)
        {
            mouseInput.x = -mouseInput.x;
        }

        if (invertedMouseY)
        {
            mouseInput.y = -mouseInput.y;
        }

        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            y:transform.rotation.eulerAngles.y + mouseInput.x,
            transform.eulerAngles.z);


        cameraTransform.rotation = Quaternion.Euler(
            cameraTransform.eulerAngles + new Vector3(x:-mouseInput.y, y:0f, z:0f)
            );
    }
}
