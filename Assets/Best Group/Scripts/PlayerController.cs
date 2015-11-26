using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpSpeed = 3.0f;
    private bool frozen = false;

    public float mouseSensitivty = 3;

    private Vector3 startPosition;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {

        // Get the controller
        CharacterController controller = GetComponent<CharacterController>();

        // Check if the controller is on the ground
        if (controller.isGrounded && !frozen)
        {

            moveDirection = transform.forward * Input.GetAxis("Vertical");
            moveDirection += transform.right * Input.GetAxis("Horizontal");

            // If moveDirection vector is better than 1, normalize it so it doesn't go faster walking diagonally
            if(moveDirection.magnitude > 1)
            {
                moveDirection = transform.TransformDirection(moveDirection.normalized);
            }
            else
            {
                moveDirection = transform.TransformDirection(moveDirection);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection = moveDirection * runSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkSpeed;
            }

            // Jumping
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

        }

        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;

        // Move
        controller.Move(moveDirection * Time.deltaTime);

        // Turn off application on escape
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    public void restart()
    {
        transform.position = startPosition;
        //StartCoroutine(freeze());
    }
    
    public bool isFrozen()
    {
        return frozen;
    }

    IEnumerator freeze()
    {
        frozen = true;
        yield return new WaitForSeconds(2.0f);
        frozen = false;
    }
}
