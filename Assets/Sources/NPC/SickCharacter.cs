using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SickCharacter : MonoBehaviour
{
    [Range(0, 50)]
    [SerializeField] private int _cureCost;
    [SerializeField] private Drug _needDrug;

    private NavMeshAgent _agent;
    private bool _isDrugFounded;

    public int CureCost => _cureCost;
    public Drug NeedDrug => _needDrug;
    public NavMeshAgent Agent => _agent;
    public bool IsDrugFounded => _isDrugFounded;

    public event Action Issued;
    public event Action<int> RepayCure;
    public event Action<SickCharacter> NeedHangOn;
    public event Action ReadyForCure;
    public event Action<Transform, Transform, Transform> SeatDefined;
    public event Action<List<Drug>> CureStarted;

    public void HangOn()
    {
        NeedHangOn?.Invoke(this);
    }

    public void InizializeParameters(Transform seatPosition, Transform leavePosition)
    {
        _agent = GetComponent<NavMeshAgent>();
        _isDrugFounded = false;
        SeatDefined?.Invoke(seatPosition, leavePosition, null);
    }

    public void SetBedPosition(Transform bedPosition)
    {
        SeatDefined?.Invoke(null, null, bedPosition);
        ReadyForCure?.Invoke();
    }

    public void StartCure(List<Drug> drugs)
    {
        CureStarted?.Invoke(drugs);
    }

    public void Issue()
    {
        Issued?.Invoke();
        RepayCure?.Invoke(_cureCost);
        _isDrugFounded = true;
    }
}
