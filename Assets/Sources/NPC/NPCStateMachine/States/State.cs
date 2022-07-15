using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SickCharacter))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Animator Animator;
    protected SickCharacter SickCharacter;

    public void InizializeState()
    {
        enabled = true;

        foreach (Transition transition in _transitions)
            transition.enabled = true;
    }

    public void FinalizeState()
    {
        foreach (Transition transition in _transitions)
            transition.enabled = false;

        enabled = false;
    }

    public bool Check—ompletionState()
    {
        foreach (Transition transition in _transitions)
        {
            if (transition.NeedTransit)
                return true;
        }

        return false;
    }

    public State GetNextState()
    {
        foreach (Transition transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.NextState;
        }

        return null;
    }

    private void OnEnable()
    {
        Animator = GetComponent<Animator>();
        SickCharacter = GetComponent<SickCharacter>();
    }
}
