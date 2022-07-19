using UnityEngine;

public class NPCLocationHandler : MonoBehaviour
{
    private SickCharacter _sickCharacter;
    private Transform _seatPosition;
    private Transform _exitPosition;
    private Transform _bedPosition;

    public Transform SeatPosition => _seatPosition;
    public Transform ExitPosition => _exitPosition;
    public Transform BedPosition => _bedPosition;

    private void OnEnable()
    {
        _sickCharacter.SeatDefined += OnPositionDefined;
    }

    private void Awake()
    {
        _sickCharacter = GetComponent<SickCharacter>();
    }

    private void OnDisable()
    {
        _sickCharacter.SeatDefined -= OnPositionDefined;
    }

    private void OnPositionDefined(Transform seatPosition, Transform exitPosition, Transform bedPosition)
    {
        if (_seatPosition == null)
            _seatPosition = seatPosition;

        if (_exitPosition == null)
            _exitPosition = exitPosition;

        if (_bedPosition == null)
            _bedPosition = bedPosition;
    }
}
