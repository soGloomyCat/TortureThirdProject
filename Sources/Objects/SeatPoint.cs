using UnityEngine;

public class SeatPoint : MonoBehaviour
{
    private bool _isOccupied;
    private SickCharacter _sickCharacter;

    public bool IsOccupied => _isOccupied;
    public Transform Position => transform;

    public void OccupySeat(SickCharacter sickCharacter)
    {
        _isOccupied = true;
        _sickCharacter = sickCharacter;
    }

    public void ExemptSeat(SickCharacter sickCharacter)
    {
        if (_sickCharacter == sickCharacter)
        {
            _sickCharacter = null;
            _isOccupied = false;
        }
    }
}
