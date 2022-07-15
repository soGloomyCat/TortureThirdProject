using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    private const string AnimationLabel = "IsOpen";
    private const float Offset = 2f;

    [SerializeField] private Drug _drug;
    [SerializeField] private TakeIcon _originalTakeIcon;
    [SerializeField] private float _issueTime;

    private Animator _animator;
    private TakeIcon _takeIcon;

    public event Action<Drug> RequiredIssueDrug;
    public event Action<Transform> TransferSpawnPoint;

    public void BeginIssue()
    {
        _takeIcon.transform.position = new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z);
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
        _takeIcon = Instantiate(_originalTakeIcon);
        _takeIcon.Complete += IssueDrug;
    }

    private void OnDisable()
    {
        _takeIcon.Complete -= IssueDrug;
    }
}
