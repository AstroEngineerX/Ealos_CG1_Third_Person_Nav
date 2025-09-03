using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;//Reference to the PlayerInput component.
    [SerializeField] private AgentMover _movement;//Reference to the AgentMover component.
    [SerializeField] private AgentAnimation _agentAnimation;//Reference to the AgentAnimation component.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input.OnMouseClick += _movement.SetDestination;//Subscribe to the OnMouseClick event of the PlayerInput component. Calls the SetDestination method of the AgentMover component when the event is triggered.
        _movement.OnSpeedChange += _agentAnimation.SetSpeed;//Subscribe to the OnSpeedChange event of the AgentMover component. Calls the SetSpeed method of the AgentAnimation component when the event is triggered.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
