using UnityEngine;

public class SeatState : State
{
    private const float Offset = 0.3f;

    private void OnEnable()
    {
        SickAnimator.SitDown();
    }

    private void Start()
    {
        transform.position = new Vector3(LocationHandler.SeatPosition.position.x, transform.position.y + Offset, LocationHandler.SeatPosition.position.z);
        transform.rotation = Quaternion.Euler(0, 90, 0);
        SickCharacter.Agent.enabled = false;
    }
}
