using UnityEngine;

[RequireComponent(typeof(EnterState))]
public class SeatTransition : Transition
{
    private EnterState _enterState;

    private void OnEnable()
    {
        _enterState.Reached += ChangeTransitStatus;
    }

    private void Awake()
    {
        _enterState = GetComponent<EnterState>();
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
