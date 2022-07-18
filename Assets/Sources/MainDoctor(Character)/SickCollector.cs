using System;
using UnityEngine;

[RequireComponent(typeof(SickPoolHandler))]
[RequireComponent(typeof(Doctor))]
[RequireComponent(typeof(DoctorAnimationHandler))]
public class SickCollector : MonoBehaviour
{
    private SickPoolHandler _poolHandler;
    private Doctor _doctor;
    private DoctorAnimationHandler _animator;

    public event Action PickedUp;
    public event Action DropDown;
    public event Action<SickCharacter> ExemptSeat;

    private void Awake()
    {
        _poolHandler = GetComponent<SickPoolHandler>();
        _doctor = GetComponent<Doctor>();
        _animator = GetComponent<DoctorAnimationHandler>();
    }

    private void OnTriggerEnter(Collider interectiveObject)
    {
        if (interectiveObject.TryGetComponent(out Bed bed))
        {
            if (_poolHandler.Fulness > 0 && bed.IsOccupied == false)
            {
                bed.ReceiveSick(_poolHandler.GetLastSick());
            }

            CheckCargoAvailability();
        }

        if (interectiveObject.TryGetComponent(out TrashBox trashBox))
        {
            _poolHandler.ClearPool();
        }

    }

    public void PrepairHangOnSick(SickCharacter sick)
    {
        ExemptSeat?.Invoke(sick);
        _animator.EnableHangAnimation();
        _poolHandler.HangOnSick(sick);
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
