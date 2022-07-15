using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{
    [SerializeField] private List<SeatPoint> _seats;

    public Transform GetFreeSeat(SickCharacter sickCharacter)
    {
        foreach (SeatPoint seat in _seats)
        {
            if (seat.IsOccupied == false)
            {
                seat.OccupySeat(sickCharacter);
                return seat.Position;
            }
        }

        return null;
    }

    public bool CheckPresenceFreeSeat()
    {
        foreach (SeatPoint seat in _seats)
        {
            if (seat.IsOccupied == false)
                return true;
        }

        return false;
    }
}
