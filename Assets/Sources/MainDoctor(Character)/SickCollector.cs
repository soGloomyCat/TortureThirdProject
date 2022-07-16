using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Doctor))]
[RequireComponent(typeof(Animator))]
public class SickCollector : MonoBehaviour
{
    private const string AnimationTrigger = "WithCargo";
    private const float Offset = 0.15f;
    private int TriggerValue = 1;

    [SerializeField] private Transform _sickPool;

    private Doctor _doctor;
    private Animator _animator;
    private SickCharacter _lastsick;
    private List<SickCharacter> _sickCharacters;
    private int _pickedSickCount;

    public int Fulness => _sickCharacters.Count;

    public event Action PickedUp;
    public event Action DropDown;
    public event Action<SickCharacter> ExemptSeat;

    public void PrepairHangOnSick(SickCharacter sick)
    {
        ExemptSeat?.Invoke(sick);
        _lastsick = sick;
        _lastsick.gameObject.transform.parent = _sickPool;
        _lastsick.gameObject.transform.rotation = _sickPool.rotation;
        _sickCharacters.Add(_lastsick);
        HangSick();
    }

    private void HangSick()
    {
        _animator.SetBool(AnimationTrigger, true);

        if (_sickPool.childCount <= TriggerValue)
        {
            _sickCharacters[_pickedSickCount].gameObject.transform.position = _sickPool.position;
        }
        else
        {
            _sickCharacters[_pickedSickCount].gameObject.transform.position = new Vector3(_sickPool.transform.position.x,
                                                                                          _sickPool.transform.position.y + Offset * _pickedSickCount,
                                                                                          _sickPool.transform.position.z);
        }

        _sickCharacters[_pickedSickCount++].transform.localRotation = Quaternion.Euler(0, -82, 0);
        PickedUp?.Invoke();
    }

    private void OnEnable()
    {
        if (_sickPool == null)
            throw new ArgumentNullException("Отсутствует обязательный компонент. Проверьте инспектор.");

        _doctor = GetComponent<Doctor>();
        _animator = GetComponent<Animator>();
        _sickCharacters = new List<SickCharacter>();
        _pickedSickCount = 0;
    }

    private void OnTriggerEnter(Collider interectiveObject)
    {
        if (interectiveObject.TryGetComponent(out Bed bed))
        {
            if (_sickCharacters.Count > 0 && bed.IsOccupied == false)
            {
                bed.ReceiveSick(_sickCharacters[_sickCharacters.Count - TriggerValue]);
                _sickCharacters.RemoveAt(_sickCharacters.Count - TriggerValue);
            }

            if (_doctor.CheckCargoPresence())
            {
                DropDown?.Invoke();
                _animator.SetBool(AnimationTrigger, false);
            }

            _pickedSickCount = _sickCharacters.Count;
        }

        if (interectiveObject.TryGetComponent(out TrashBox trashBox))
        {
            foreach (var item in _sickCharacters)
                Destroy(item.gameObject);

            _pickedSickCount = 0;
            _sickCharacters.Clear();
        }

    }
}
