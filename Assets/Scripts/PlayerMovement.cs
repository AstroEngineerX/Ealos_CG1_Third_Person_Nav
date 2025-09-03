using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;
    AnimatorManager animatorManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;
    public float walkingSpeed = 2.5f;
    public float runningSpeed = 7f;
    public float rotationSpeed = 14f;
    public bool isRunning;

    public float inAirTime;// Build up speed while falling
    public float leapingVelocity;// Velocity while leaping
    public float fallingVelocity;// Velocity while falling
    public float rayCastHeightOffset = 0.5f;// Height offset for raycasting to detect ground
    public LayerMask groundLayer;// Layer mask to identify ground objects
    public float maxDistance = 1;// Starting point for ground detection raycast
    public bool isGrounded;// Is the player grounded
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.y = 0;
        moveDirection.Normalize();
        if (isRunning)
            moveDirection *= runningSpeed;
        else
            moveDirection *= walkingSpeed;
        Vector3 movementVelocity = moveDirection;
        playerRigidbody.linearVelocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection.y = 0;
        targetDirection.Normalize();
        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting)
            return;
        HandleMovement();
        HandleRotation();
    }

    public void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y += rayCastHeightOffset;

        if (!isGrounded)
        {
            if (!playerManager.isInteracting)
                animatorManager.PlayerTargetAnimation("Falling", true);

            inAirTime += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTime);//makes the player fall faster over time
        }

        if (Physics.SphereCast(rayCastOrigin, 0.1f, Vector3.down, out hit, maxDistance, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
                animatorManager.PlayerTargetAnimation("Landing", true);

                inAirTime = 0;//reset inAirTime once we land
                isGrounded = true;
                playerManager.isInteracting = false;
        }else
        {
            isGrounded = false;
        }
    }
}
