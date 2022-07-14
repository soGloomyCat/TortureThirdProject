using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    private const string AnimationLabel = "IsOpen";

    [SerializeField] private Drug _drug;
    [SerializeField] private TakeIcon _takeIcon;
    [SerializeField] private float _issueTime;

    private Animator _animator;

    public event Action<Drug> RequiredIssueDrug;
    public event Action<Transform> TransferSpawnPoint;

    public void BeginIssue()
    {
        _takeIcon.gameObject.SetActive(true);
        _takeIcon.PrepairActivate(_issueTime);
    }

    public void StopIssue()
    {
        _takeIcon.PrepairDeactivate();
    }

    private void IssueDrug()
    {
        _animator.SetTrigger(AnimationLabel);
        TransferSpawnPoint?.Invoke(transform);
        RequiredIssueDrug?.Invoke(_drug);
    }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _takeIcon.Complete += IssueDrug;
    }

    private void OnDisable()
    {
        _takeIcon.Complete -= IssueDrug;
    }
}
