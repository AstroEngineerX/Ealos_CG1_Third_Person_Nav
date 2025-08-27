using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerController;
    public Vector2 movementInput;

    public float verticalInput;
    public float horizontalInput;

    AnimatorManager animatorManager;
    public float moveAmount;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable()
    {
        if (playerController == null)
        {
            playerController = new PlayerController();
            playerController.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
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
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        //HandlejumpInput();
        //Handle any other input
    }
}
