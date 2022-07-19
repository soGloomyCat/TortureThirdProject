using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _balance;

    public event Action<int> BalanceChanged;

    private void Start()
    {
        _balance = 0;
        BalanceChanged?.Invoke(_balance);
    }

    public void ChangeBalance(int reward)
    {
        _balance += reward;
        BalanceChanged?.Invoke(_balance);
    }
}
