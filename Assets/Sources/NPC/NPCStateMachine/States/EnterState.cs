using System;

public class EnterState : State
{
    public event Action Reached;

    private void Start()
    {
        SickCharacter.Agent.SetDestination(LocationHandler.SeatPosition.position);
    }

    private void Update()
    {
        if (transform.position == SickCharacter.Agent.destination)
        {
            SickCharacter.Agent.enabled = false;
            Reached?.Invoke();
        }
    }
}
