using System;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour
{
    [SerializeField] private List<SeatPoint> _seats;

    public event Action<Transform> TakeSeatPosition;

    public void GetFreeSeat(SickCharacter sickCharacter)
    {
        foreach (SeatPoint seat in _seats)
        {
            if (seat.IsOccupied == false)
            {
                seat.OccupySeat(sickCharacter);
                TakeSeatPosition?.Invoke(seat.Position);
                break;
            }
        }
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

    private void OnEnable()
    {
        if (_seats == null)
            throw new ArgumentNullException("Отсутствует обязательный компонент. Проверьте инспектор.");
    }
}
