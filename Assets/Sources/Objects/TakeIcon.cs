using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TakeIconAnimator))]
public class TakeIcon : MonoBehaviour
{
    [SerializeField] private Image _mainIcon;
    [SerializeField] private Image _fillIcon;

    private Timer _timer;
    private TakeIconAnimator _animator;

    public event Action Completed;

    public void Init(Timer timer)
    {
        if (_timer != null)
            UnsubscribeTimer();

        _timer = timer;
        _animator = GetComponent<TakeIconAnimator>();
        SubscribeTimer();
    }

    private void SubscribeTimer()
    {
        _timer.Started += OnTimerStart;
        _timer.Updated += OnTimerUpdate;
        _timer.Stopped += OnTimerStopped;
        _timer.Completed += OnTimerCompleted;
    }

    private void UnsubscribeTimer()
    {
        _timer.Started -= OnTimerStart;
        _timer.Updated -= OnTimerUpdate;
        _timer.Stopped -= OnTimerStopped;
        _timer.Completed -= OnTimerCompleted;
    }

    private void OnTimerStart()
    {
        _animator.Activate();
    }

    private void OnTimerUpdate()
    {
        _fillIcon.fillAmount = _timer.TimeLeft;
    }

    private void OnTimerStopped()
    {
        _animator.Deactivate();
    }

    private void OnTimerCompleted()
    {
        Completed?.Invoke();
    }
}

