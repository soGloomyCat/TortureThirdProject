using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private Transform _bedPosition;
    [SerializeField] private ParticleSystem _particleSystem;

    private SickCharacter _sick;
    private bool _isOccupied;

    public bool IsOccupied => _isOccupied;

    public void ReceiveSick(SickCharacter sick)
    {
        _sick = sick;
        _sick.SetBedPosition(_bedPosition);
        _isOccupied = true;
        _sick.Issued += DischargeSick;
    }

    public void FoundNeedDrug(List<Drug> drugs)
    {
        _sick.StartCure(drugs);
    }

    private void DischargeSick()
    {
        _particleSystem.Play();
        _isOccupied = false;
        _sick.Issued -= DischargeSick;
    }
}
