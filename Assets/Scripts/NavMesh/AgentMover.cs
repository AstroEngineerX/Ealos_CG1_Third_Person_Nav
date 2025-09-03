using UnityEngine;
using System;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _Agent;

    public event Action <float> OnSpeedChange;

    public void SetDestination(Vector3 destination)
    {
        _Agent.destination = destination;//Navmesh stores the destination and moves the agent automatically
    }

    // Update is called once per frame
    void Update()
    {
        OnSpeedChange?.Invoke(Mathf.Clamp01(_Agent.velocity.magnitude / _Agent.speed));//? Verifies is there a subscriber. Clamp01: get a value between 0 and 1
    }
}
