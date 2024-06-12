using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float groundCheckDistance = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    public Animator animator; // Reference to the Animator component

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !Score_Timer.stopTimer)
        {
            GameManager.gamePaused = !GameManager.gamePaused;
            Time.timeScale = GameManager.gamePaused ? 0 : 1;
        }

        if (!GameManager.gamePaused)
        {
            Move();
            Jump();
        }
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Determine if the player is moving
        bool isMoving = moveHorizontal != 0 || moveVertical != 0;
        animator.SetBool("isRunning", isMoving && isGrounded); // Update the Animator parameter

        // Get the forward and right vectors of the camera
        Vector3 cameraForward = CameraSwitcher.ActvCam.transform.forward;
        Vector3 cameraRight = CameraSwitcher.ActvCam.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        float speed = GameManager.speedValue * (1 + GameManager.speedPerc / 100);
        Vector3 movement = (cameraForward * moveVertical + cameraRight * moveHorizontal) * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + movement;
        rb.MovePosition(newPosition);

        Vector3 offset = new Vector3(0f, 0.75f, 0f);
        CameraSwitcher.cams[0].transform.position = newPosition + offset + cameraForward * 0.5f;

        // Offset the camera position from the player
        offset = new Vector3(0f, 1.391f, -7.6805f);

        CameraSwitcher.cams[1].transform.position = newPosition + transform.rotation * offset;
        CameraSwitcher.cams[1].transform.rotation = transform.rotation; // Rotate camera based on player rotation
    }

    void Jump()
    {
        isGrounded = Physics.CheckCapsule(transform.position, transform.position + Vector3.down * groundCheckDistance, 0.1f, groundLayer);
        animator.SetBool("isJumping", !isGrounded); // Update the Animator parameter
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
}