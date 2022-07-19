using System.Collections.Generic;
using UnityEngine;

public class EventsHandler : MonoBehaviour
{
    [SerializeField] private DrugCollector _drugCollector;
    [SerializeField] private SickCollector _sickCollector;
    [SerializeField] private List<SeatPoint> _seats;
    [SerializeField] private List<Chest> _chest;
    [SerializeField] private NPCSpawner _spawner;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private UIHandler _uIHandler;

    private List<SickCharacter> _tempCharacters;

    private void OnEnable()
    {
        _tempCharacters = new List<SickCharacter>();

        foreach (var chest in _chest)
        {
            chest.RequiredIssueDrug += _drugCollector.AcceptDrug;
            chest.TransferSpawnPoint += _drugCollector.SetDrugSpawnPoint;
        }

        foreach (var seat in _seats)
        {
            _sickCollector.ExemptSeat += seat.ExemptSeat;
        }

        _spawner.SpawnedNewSick += SubscribeSick;
        _drugCollector.PickedUp += _uIHandler.EnableIcon;
        _drugCollector.DropDown += _uIHandler.DisableIcon;
        _sickCollector.PickedUp += _uIHandler.EnableIcon;
        _sickCollector.DropDown += _uIHandler.DisableIcon;
        _wallet.BalanceChanged += _uIHandler.ChangeBalanceText;
    }

    private void OnDisable()
    {
        foreach (var chest in _chest)
        {
            chest.RequiredIssueDrug -= _drugCollector.AcceptDrug;
            chest.TransferSpawnPoint -= _drugCollector.SetDrugSpawnPoint;
        }

        foreach (var seat in _seats)
        {
            _sickCollector.ExemptSeat -= seat.ExemptSeat;
        }

        _spawner.SpawnedNewSick -= SubscribeSick;
        UnsubscribeSick();
        _drugCollector.PickedUp -= _uIHandler.EnableIcon;
        _drugCollector.DropDown -= _uIHandler.DisableIcon;
        _sickCollector.PickedUp -= _uIHandler.EnableIcon;
        _sickCollector.DropDown -= _uIHandler.DisableIcon;
        _wallet.BalanceChanged += _uIHandler.ChangeBalanceText;
    }

    private void SubscribeSick(SickCharacter sick)
    {
        sick.RepayCure += _wallet.ChangeBalance;
        sick.NeedHangOn += _sickCollector.PrepairHangOnSick;
        _tempCharacters.Add(sick);
    }

    private void UnsubscribeSick()
    {
        foreach (var sick in _tempCharacters)
        {
            sick.RepayCure -= _wallet.ChangeBalance;
            sick.NeedHangOn -= _sickCollector.PrepairHangOnSick;
        }
    }
}
