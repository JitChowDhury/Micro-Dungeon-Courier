using UnityEngine;

// Ensures a CharacterController is attached to the GameObject
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Movement settings configurable in the Unity Inspector
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f; // Height of the jump (m)
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float rotationSpeed = 720f; // Rotation speed 
    [SerializeField] private Transform cameraTransform;
    // Component references
    private CharacterController controller;
    private Animator animator;

    // Movement state
    private Vector3 verticalVelocity; // Tracks vertical velocity for jumping and gravity
    private bool isGrounded; // Whether the player is on the ground
    private Transform currentPlatform; // The moving platform the player is on, if any
    private Vector3 lastPlatformPosition; // Last position of the platform for movement calculation

    // Initialize components and lock cursor
    private void Start()
    {
        // Get required components
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        UpdateGroundedState();
        HandlePlayerInput();
        HandlePlatformMovement();
        ApplyGravity();
        UpdateAnimations();
    }

    // Check if the player is grounded and reset vertical velocity if needed
    private void UpdateGroundedState()
    {
        isGrounded = controller.isGrounded;

        // Apply a small downward force when grounded to prevent floating
        if (isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
        }
    }

    // Process player movement and rotation based on input
    private void HandlePlayerInput()
    {
        // Get raw input for horizontal and vertical movement
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        // Calculate movement direction relative to camera
        Vector3 moveDirection = CalculateCameraRelativeDirection(inputX, inputZ);

        // Rotate player to face movement direction
        if (moveDirection.magnitude > 0.1f)
        {
            RotatePlayer(moveDirection);
        }

        // Combine player and platform movement
        Vector3 totalMovement = CalculateTotalMovement(moveDirection);

        // Apply movement
        controller.Move(totalMovement);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PerformJump();
        }
    }

    // Calculate movement direction based on camera orientation
    private Vector3 CalculateCameraRelativeDirection(float inputX, float inputZ)
    {
        // Project camera forward and right vectors onto XZ plane and normalize
        Vector3 camForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(cameraTransform.right, new Vector3(1, 0, 1)).normalized;

        // Combine inputs to get movement direction
        return (camForward * inputZ + camRight * inputX).normalized;
    }

    // Rotate the player to face the movement direction
    private void RotatePlayer(Vector3 moveDirection)
    {
        // Calculate target rotation based on movement direction
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Calculate the platform's movement contribution
    private void HandlePlatformMovement()
    {
        if (currentPlatform == null) return;

        // Calculate platform movement since last frame
        Vector3 newPlatformPosition = currentPlatform.position;
        Vector3 platformDelta = newPlatformPosition - lastPlatformPosition;
        lastPlatformPosition = newPlatformPosition;
    }

    // Combine player movement with platform movement
    private Vector3 CalculateTotalMovement(Vector3 moveDirection)
    {
        // Calculate platform velocity (if on a platform)
        Vector3 platformVelocity = currentPlatform != null ? (currentPlatform.position - lastPlatformPosition) / Time.deltaTime : Vector3.zero;

        // Combine player movement and platform velocity, scaled by frame time
        return (moveDirection * moveSpeed + platformVelocity) * Time.deltaTime;
    }

    // Handle jumping mechanics
    private void PerformJump()
    {
        // Calculate initial jump velocity to reach desired height
        verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    // Apply gravity to vertical velocity and move the player
    private void ApplyGravity()
    {
        // Update vertical velocity with gravity
        verticalVelocity.y += gravity * Time.deltaTime;

        // Apply vertical movement
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    // Update animator parameters for movement and jumping
    private void UpdateAnimations()
    {
        // Set animation parameters
        animator.SetFloat("Speed", CalculateCameraRelativeDirection(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).magnitude);
        animator.SetBool("IsJumping", !isGrounded);
    }

    // Handle collisions with platforms and pushable objects
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Handle moving platforms
        if (hit.collider.CompareTag("MovingPlatform"))
        {
            if (currentPlatform != hit.collider.transform)
            {
                // Attach to the new platform
                currentPlatform = hit.collider.transform;
                lastPlatformPosition = currentPlatform.position;
                transform.SetParent(currentPlatform);
            }
        }
        else if (currentPlatform != null)
        {
            // Detach from the platform if colliding with something else
            transform.SetParent(null);
            currentPlatform = null;
        }

        // Handle pushing objects
        Rigidbody body = hit.collider.attachedRigidbody;


        if (body == null || body.isKinematic) return;


        if (!hit.collider.CompareTag("Pushable")) return;

        // Avoid pushing if moving significantly downward
        if (hit.moveDirection.y < -0.3f) return;

        // Apply push force in the movement direction (XZ plane)
        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.linearVelocity = pushDirection * 3f; // Push strength
    }
}