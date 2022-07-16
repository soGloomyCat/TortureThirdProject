using UnityEngine;

[RequireComponent(typeof(SickCharacter))]
public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _nextState;

    protected SickCharacter SickCharacter;

    public State NextState => _nextState;
    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        if (_nextState == null)
            throw new System.ArgumentNullException("Отсутствует обязательный компонент. Проверьте инспектор.");

        SickCharacter = GetComponent<SickCharacter>();
        NeedTransit = false;
    }
}
