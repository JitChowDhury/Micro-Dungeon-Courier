using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement
    public float jumpForce = 5f; // Force applied when jumping
    public float rotationSpeed = 720f; // Speed of rotation towards movement direction
    private Rigidbody rb;
    private Animator animator; // Reference to Animator component
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        // Keyboard-only movement input (WASD or arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or left/right arrow
        float moveZ = Input.GetAxisRaw("Vertical"); // W/S or up/down arrow
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Move the player based on keyboard input
        if (moveDirection.magnitude > 0)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // Rotate towards movement direction (no mouse input)
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Set animation parameters
        animator.SetFloat("Speed", moveDirection.magnitude); // Set Speed for Idle/Walk
        animator.SetBool("IsJumping", !isGrounded); // Set IsJumping for Jump

        // Jump with Space key
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}