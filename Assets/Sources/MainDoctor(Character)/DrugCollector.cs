using System;
using UnityEngine;

[RequireComponent(typeof(DrugPoolHandler))]
[RequireComponent(typeof(Doctor))]
[RequireComponent(typeof(DoctorAnimationHandler))]
[RequireComponent(typeof(Rigidbody))]
public class DrugCollector : MonoBehaviour
{
    private DrugPoolHandler _poolHandler;
    private Doctor _doctor;
    private DoctorAnimationHandler _animator;
    private Rigidbody _rigidbody;

    public event Action PickedUp;
    public event Action DropDown;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _poolHandler = GetComponent<DrugPoolHandler>();
        _doctor = GetComponent<Doctor>();
        _animator = GetComponent<DoctorAnimationHandler>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider interactiveObject)
    {
        if (interactiveObject.TryGetComponent(out Bed bed))
        {
            if (_poolHandler.Fulness > 0 && bed.IsOccupied)
            {
                bed.FoundNeedDrug(_poolHandler.Drugs);
                _poolHandler.OverrideDrugsposition();
            }

            CheckCargoAvailability();
        }

        if (interactiveObject.TryGetComponent(out TrashBox trashBox))
        {
            _poolHandler.ClearPool();
            _animator.DisableHangAnimation();
        }

        if (interactiveObject.TryGetComponent(out Chest chest))
        {
            chest.BeginIssue();
        }
    }

    private void OnTriggerStay(Collider interactiveObject)
    {
        if (interactiveObject.TryGetComponent(out Chest chest))
            chest.ContinueIssue();
    }

    private void OnTriggerExit(Collider interactiveObject)
    {
        if (interactiveObject.TryGetComponent(out Chest chest))
            chest.StopIssue();
    }

    public void SetDrugSpawnPoint(Transform spawnPoint)
    {
        _poolHandler.SetDrugSpawnPoint(spawnPoint);
    }

    public void AcceptDrug(Drug drug)
    {
        _poolHandler.AcceptDrug(drug);
        _animator.EnableHangAnimation();
        PickedUp?.Invoke();
    }

    private void CheckCargoAvailability()
    {
        if (_doctor.HasCargo() == false)
        {
            DropDown?.Invoke();
            _animator.DisableHangAnimation();
        }
    }
}
