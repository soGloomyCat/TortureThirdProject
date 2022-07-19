using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCAnimationHandler))]
[RequireComponent(typeof(SickCharacter))]
[RequireComponent(typeof(NPCLocationHandler))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected NPCAnimationHandler SickAnimator { get; private set; }
    protected SickCharacter SickCharacter { get; private set; }
    protected NPCLocationHandler LocationHandler { get; private set; }

    private void Awake()
    {
        SickAnimator = GetComponent<NPCAnimationHandler>();
        SickCharacter = GetComponent<SickCharacter>();
        LocationHandler = GetComponent<NPCLocationHandler>();
    }

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

    public bool ReadyToTransit()
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
}
