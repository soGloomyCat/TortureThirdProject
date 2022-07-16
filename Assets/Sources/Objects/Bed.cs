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
        _sick.gameObject.transform.parent = _bedPosition;
        _sick.gameObject.transform.position = _bedPosition.position;
        _sick.gameObject.transform.rotation = _bedPosition.rotation;
        _sick.GetBedPosition(_bedPosition);
        _isOccupied = true;
        _sick.Issued += DischargeSick;
    }

    public void FoundNeedDrug(List<Drug> drugs)
    {
        _sick.FoundCorrectnessDrug(drugs);
    }

    private void OnEnable()
    {
        if (_bedPosition == null || _particleSystem == null)
            throw new System.ArgumentNullException("Отсутствует обязательный компонент. Проверьте инспектор.");
    }

    private void DischargeSick()
    {
        _particleSystem.Play();
        _isOccupied = false;
        _sick.Issued -= DischargeSick;
    }
}
