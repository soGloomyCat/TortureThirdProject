using System;
using System.Collections.Generic;
using UnityEngine;

public class SickCharacter : MonoBehaviour
{
    [SerializeField] private Drug _needDrug;
    [SerializeField] private SpriteRenderer _drugIcon;
    [SerializeField] private Reward _rewardPrefab;
    [SerializeField] private Reward _specialReward;
    [SerializeField] private int _cureCost;
    [SerializeField] private TakeIcon _takeIcon;

    private List<Transform> _wayPoints;
    private List<Transform> _exitWayPoints;
    private Bench _bench;
    private bool _isDrugFounded;
    private Transform _iconPosition;
    private Transform _rewardPosition;

    public List<Transform> WayPoints => _wayPoints;
    public List<Transform> ExitWayPoints => _exitWayPoints;
    public Bench Bench => _bench;
    public Drug NeedDrug => _needDrug;
    public bool IsDrugFounded => _isDrugFounded;
    public TakeIcon TakeIcon => _takeIcon;

    public event Action Issued;
    public event Action<int> RepayCure;
    public event Action<SickCharacter> NeedHangOn;

    public void HangOn()
    {
        NeedHangOn?.Invoke(this);
    }

    public void InizializeParameters(Transform way, Transform exitWay, Bench bench)
    {
        _wayPoints = new List<Transform>();
        _exitWayPoints = new List<Transform>();
        _bench = bench;

        for (int i = 0; i < way.childCount; i++)
        {
            _wayPoints.Add(way.GetChild(i));
        }

        for (int i = 0; i < exitWay.childCount; i++)
        {
            _exitWayPoints.Add(exitWay.GetChild(i));
        }

        _isDrugFounded = false;
    }

    public void ShowDisease()
    {
        _drugIcon.gameObject.transform.position = _iconPosition.position;
        _drugIcon.gameObject.transform.rotation = _iconPosition.rotation;
        _drugIcon.gameObject.SetActive(true);
    }

    public void FoundCorrectnessDrug(List<Drug> drugs)
    {
        foreach (var drug in drugs)
        {
            if (_needDrug.Label == drug.Label)
            {
                drug.Use();
                _isDrugFounded = true;
                _drugIcon.gameObject.SetActive(false);
                TakeReward();
                Issued?.Invoke();
                RepayCure?.Invoke(_cureCost);
            }
        }
    }

    public void SetBedPosition(Transform iconPosition, Transform rewardPosition)
    {
        _iconPosition = iconPosition;
        _rewardPosition = rewardPosition;
    }

    private void TakeReward()
    {
        Reward tempReward;
        int currentSpawnIndex;

        currentSpawnIndex = 0;

        if (_specialReward != null)
        {
            tempReward = Instantiate(_specialReward, _rewardPosition);
            tempReward.PrepairToss();
        }

        while (currentSpawnIndex < _cureCost)
        {
            tempReward = Instantiate(_rewardPrefab, _rewardPosition);
            tempReward.Dump();
            currentSpawnIndex++;
        }
    }
}
