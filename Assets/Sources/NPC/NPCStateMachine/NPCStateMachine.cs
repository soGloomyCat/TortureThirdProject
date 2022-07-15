using UnityEngine;

public class NPCStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;

    private State _currentState;
    private State _nextState;

    private void Start()
    {
        _currentState = _startState;
        _currentState.InizializeState();
    }

    private void Update()
    {
        if (_currentState.Check—ompletionState())
        {
            SetNextState();
            ChangeCurrentState();
        }
    }

    private void SetNextState()
    {
        _nextState = _currentState.GetNextState();
    }

    private void ChangeCurrentState()
    {
        _currentState.FinalizeState();

        if (_nextState != null)
        {
            _currentState = _nextState;
            _currentState.InizializeState();
        }
    }
}
