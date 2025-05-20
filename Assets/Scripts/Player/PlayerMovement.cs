using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float rotationSpeed = 720f;

    public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;

    private Vector3 velocity;
    private bool isGrounded;
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition; // Track platform position to calculate movement

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 camForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(cameraTransform.right, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDirection = (camForward * moveZ + camRight * moveX).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Calculate platform movement if on a platform
        Vector3 platformDelta = Vector3.zero;
        if (currentPlatform != null)
        {
            Vector3 newPlatformPosition = currentPlatform.position;
            platformDelta = newPlatformPosition - lastPlatformPosition;
            lastPlatformPosition = newPlatformPosition;
        }

        // Combine player movement with platform movement
        Vector3 totalMovement = (moveDirection * moveSpeed + platformDelta / Time.deltaTime) * Time.deltaTime;

        controller.Move(totalMovement);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetBool("IsJumping", !isGrounded);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("MovingPlatform"))
        {
            if (currentPlatform != hit.collider.transform)
            {
                currentPlatform = hit.collider.transform;
                lastPlatformPosition = currentPlatform.position; // Initialize platform position
                transform.SetParent(currentPlatform);
            }
        }
        else
        {
            if (currentPlatform != null)
            {
                transform.SetParent(null);
                currentPlatform = null;
            }
        }
    }
}