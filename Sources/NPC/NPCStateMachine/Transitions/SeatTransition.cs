using UnityEngine;

[RequireComponent(typeof(EnterState))]
public class SeatTransition : Transition
{
    private EnterState _enterState;

    private void OnEnable()
    {
        _enterState = GetComponent<EnterState>();
        _enterState.Reached += ChangeTransitStatus;
    }

    private void OnDisable()
    {
        _enterState.Reached -= ChangeTransitStatus;
    }

    private void ChangeTransitStatus()
    {
        NeedTransit = true;
    }
}
