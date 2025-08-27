using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerController;
    public Vector2 movementInput;

    public float verticalInput;
    public float horizontalInput;

    AnimatorManager animatorManager;
    public float moveAmount;

    PlayerMovement playerMovement;
    public bool shiftInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        if (playerController == null)
        {
            playerController = new PlayerController();
            playerController.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerController.PlayerActions.Shift.performed += i => shiftInput = true;
            playerController.PlayerActions.Shift.canceled += i => shiftInput = false;
        }
        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerMovement.isRunning);
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleRunningInput();
    }

    public void HandleRunningInput()
        {
        if (shiftInput && moveAmount > 0.5f)
        {
            playerMovement.isRunning = true;
        }
        else
        {
            playerMovement.isRunning = false;
        }
    }
}
