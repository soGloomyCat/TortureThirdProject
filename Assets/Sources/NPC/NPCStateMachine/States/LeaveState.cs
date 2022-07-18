public class LeaveState : State
{
    private void OnEnable()
    {
        SickAnimator.LeaveOut();
    }

    private void Start()
    {
        SickCharacter.Agent.enabled = true;
        SickCharacter.Agent.SetDestination(LocationHandler.ExitPosition.position);
    }

    private void Update()
    {
        if (transform.position == SickCharacter.Agent.destination)
            Leave();
    }

    private void Leave()
    {
        Destroy(gameObject);
    }
}
