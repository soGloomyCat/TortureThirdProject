using System;
using System.Collections.Generic;
using UnityEngine;

public class SickCharacter : MonoBehaviour
{
    private const float Offset = 1.5f;

    [SerializeField] private Drug _needDrug;
    [SerializeField] private Reward _rewardPrefab;
    [SerializeField] private TakeIcon _takeIcon;
    [SerializeField] private Reward _specialReward;
    [SerializeField] private Transform _drugIcon;
    [Range(0, 50)]
    [SerializeField] private int _cureCost;

    private List<Transform> _wayPoints;
    private List<Transform> _exitWayPoints;
    private Transform _rewardPosition;
    private Transform _seatPosition;
    private bool _isDrugFounded;

    public List<Transform> WayPoints => _wayPoints;
    public List<Transform> ExitWayPoints => _exitWayPoints;
    public Transform SeatPosition => _seatPosition;
    public Drug NeedDrug => _needDrug;
    public bool IsDrugFounded => _isDrugFounded;
    public TakeIcon TakeIcon => _takeIcon;

    public event Action Issued;
    public event Action<int> RepayCure;
    public event Action<SickCharacter> NeedHangOn;
    public event Action<SickCharacter> NeedSeatPosition;

    public void GetFreeSeat(Transform seatPosition)
    {
        if (_seatPosition == null)
            _seatPosition = seatPosition;
    }

    public void HangOn()
    {
        NeedHangOn?.Invoke(this);
    }

    public void InizializeParameters(Transform way, Transform exitWay)
    {
        _wayPoints = new List<Transform>();
        _exitWayPoints = new List<Transform>();

        for (int i = 0; i < way.childCount; i++)
        {
            _wayPoints.Add(way.GetChild(i));
        }

        for (int i = 0; i < exitWay.childCount; i++)
        {
            _exitWayPoints.Add(exitWay.GetChild(i));
        }

        _isDrugFounded = false;
        NeedSeatPosition?.Invoke(this);
    }

    public void ShowDisease()
    {
        _drugIcon = Instantiate(_drugIcon);
        _drugIcon.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z);
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
                Destroy(_drugIcon.gameObject);
                TakeReward();
                Issued?.Invoke();
                RepayCure?.Invoke(_cureCost);
            }
        }
    }

    public void GetBedPosition(Transform rewardPosition)
    {
        _rewardPosition = rewardPosition;
    }

    private void OnEnable()
    {
        if (_needDrug == null || _drugIcon == null || _rewardPrefab == null || _takeIcon == null)
            throw new ArgumentNullException("Отсутствует обязательный компонент. Проверьте инспектор.");
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
