using System.Collections.Generic;
using UnityEngine;

public class CureState : State
{
    private const float Offset = 1.5f;

    [SerializeField] private Transform _drugIcon;
    [SerializeField] private Reward _rewardPrefab;
    [SerializeField] private Reward _specialReward;

    private int _cureCost;

    private void OnEnable()
    {
        SickAnimator.LieDown();
        SickCharacter.CureStarted += FoundCorrectnessDrug;
    }

    private void OnDisable()
    {
        Destroy(_drugIcon.gameObject);
    }

    private void Start()
    {
        _cureCost = SickCharacter.CureCost;
        gameObject.transform.parent = null;
        transform.position = LocationHandler.BedPosition.position;
        transform.rotation = LocationHandler.BedPosition.rotation;
        ShowDisease();
    }

    private void ShowDisease()
    {
        _drugIcon = Instantiate(_drugIcon);
        _drugIcon.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z);
        _drugIcon.gameObject.SetActive(true);
    }

    private void FoundCorrectnessDrug(List<Drug> drugs)
    {
        foreach (var drug in drugs)
        {
            if (SickCharacter.NeedDrug.Label == drug.Label)
            {
                drug.Use();
                SickCharacter.Issue();
                TakeReward();
                break;
            }
        }
    }

    private void TakeReward()
    {
        Reward tempReward;
        int currentSpawnIndex;

        currentSpawnIndex = 0;

        if (_specialReward != null)
        {
            tempReward = Instantiate(_specialReward, LocationHandler.BedPosition);
            tempReward.PrepairToss();
        }

        while (currentSpawnIndex < _cureCost)
        {
            tempReward = Instantiate(_rewardPrefab, LocationHandler.BedPosition);
            tempReward.Dump();
            currentSpawnIndex++;
        }
    }
}
