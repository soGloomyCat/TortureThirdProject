using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    private const string AnimationLabel = "IsOpen";
    private const float Offset = 2f;

    [SerializeField] private Drug _drug;
    [Range(0.1f, 10)]
    [SerializeField] private float _issueTime;
    [SerializeField] private TakeIcon _takeIcon;

    private Timer _timer;
    private Animator _animator;

    public event Action<Drug> RequiredIssueDrug;
    public event Action<Transform> TransferSpawnPoint;

    private void OnEnable()
    {
        _takeIcon.Completed += IssueDrug;
    }

    private void Awake()
    {
        _timer = new Timer();
        _animator = GetComponent<Animator>();
        _takeIcon = Instantiate(_takeIcon);
        _takeIcon.Init(_timer);
    }

    private void OnDisable()
    {
        _takeIcon.Completed -= IssueDrug;
    }

    public void BeginIssue()
    {
        _takeIcon.transform.position = new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z);
        _timer.StartCountdown(_issueTime);
    }

    public void ContinueIssue()
    {
        _timer.Tick(Time.deltaTime);
    }

    public void StopIssue()
    {
        _timer.Stop();
    }

    private void IssueDrug()
    {
        _animator.SetTrigger(AnimationLabel);
        TransferSpawnPoint?.Invoke(transform);
        RequiredIssueDrug?.Invoke(_drug);
    }
}
