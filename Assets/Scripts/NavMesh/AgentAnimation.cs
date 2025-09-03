using UnityEngine;

public class AgentAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _movementSpeed;

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(_movementSpeed, speed);//_movementSpeed: Name saved there. Speed: Parameter value
    }
}
