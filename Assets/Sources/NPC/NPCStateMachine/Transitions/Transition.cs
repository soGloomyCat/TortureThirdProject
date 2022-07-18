using UnityEngine;

[RequireComponent(typeof(SickCharacter))]
public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _nextState;

    protected SickCharacter SickCharacter;

    public State NextState => _nextState;
    public bool NeedTransit { get; protected set; }

    private void Awake()
    {
        SickCharacter = GetComponent<SickCharacter>();
        NeedTransit = false;
    }
}
