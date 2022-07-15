using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Doctor))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class DrugCollector : MonoBehaviour
{
    private const string AnimationTrigger = "WithCargo";
    private const int TriggerValue = 1;

    [SerializeField] private Transform _drugPool;

    private Doctor _doctor;
    private Animator _animator;
    private Drug _lastDrug;
    private List<Drug> _drugs;
    private Rigidbody _rigidbody;
    private Transform _spawnPoint;

    public int Fulness => _drugs.Count;

    public event Action PickedUp;
    public event Action DropDown;

    public void SetDrugSpawnPoint(Transform spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

    public void AcceptDrug(Drug drug)
    {
        Drug tempDrug;

        tempDrug = Instantiate(drug, _drugPool);
        tempDrug.ChangeStartPoint(_spawnPoint);

        if (_drugPool.childCount == TriggerValue)
        {
            tempDrug.PrepairPutIn(_drugPool, _rigidbody);
            _lastDrug = tempDrug;
            _drugs.Add(tempDrug);
        }
        else
        {
            tempDrug.PrepairPutIn(_lastDrug.MountPoint, _lastDrug.Rigidbody);
            _lastDrug = tempDrug;
            _drugs.Add(tempDrug);
        }

        _animator.SetBool(AnimationTrigger, true);
        PickedUp?.Invoke();
    }

    private void OnEnable()
    {
        _doctor = GetComponent<Doctor>();
        _animator = GetComponent<Animator>();
        _drugs = new List<Drug>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OverrideDrugsposition(List<Drug> drugs)
    {
        for (int i = drugs.Count - TriggerValue; i >= 0; i--)
        {
            if (i > 0)
                drugs[i].InizializeParameters(drugs[i - TriggerValue].MountPoint, drugs[i - TriggerValue].Rigidbody);
            else
                drugs[i].InizializeParameters(_drugPool, _rigidbody);
        }
    }

    private void OnTriggerEnter(Collider interactiveObject)
    {
        if (interactiveObject.TryGetComponent(out Bed bed))
        {
            if (_drugs.Count > 0 && bed.IsOccupied)
            {
                bed.FoundNeedDrug(_drugs);

                foreach (var drug in _drugs)
                {
                    if (drug.IsUsed == true)
                    {
                        drug.Eject();
                        _drugs.Remove(drug);
                        OverrideDrugsposition(_drugs);
                        break;
                    }
                }
            }

            if (_doctor.CheckCargoPresence())
            {
                DropDown?.Invoke();
                _animator.SetBool(AnimationTrigger, false);
            }
        }

        if (interactiveObject.TryGetComponent(out TrashBox trashBox))
        {
            foreach (var item in _drugs)
                Destroy(item.gameObject);

            _drugs.Clear();
            _animator.SetBool(AnimationTrigger, false);
        }
    }

    private void OnTriggerStay(Collider interactiveObject)
    {
        if (interactiveObject.TryGetComponent(out Chest chest))
        {
            chest.BeginIssue();
        }
    }

    private void OnTriggerExit(Collider interactiveObject)
    {
        if (interactiveObject.TryGetComponent(out Chest chest))
        {
            chest.StopIssue();
        }
    }
}
