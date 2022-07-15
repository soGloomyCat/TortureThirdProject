using System;
using System.Collections;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _balance;
    private Coroutine _coroutine;

    public event Action<int> BalanceChanged;

    public void PrepairChange(int reward)
    {
        int tempRewardValue;

        tempRewardValue = reward / 2;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeBalance(tempRewardValue));
    }

    private void Start()
    {
        _balance = 0;
        BalanceChanged?.Invoke(_balance);
    }

    private IEnumerator ChangeBalance(int intermediateValue)
    {
        WaitForSeconds waiter;

        waiter = new WaitForSeconds(0.5f);

        _balance += intermediateValue;
        BalanceChanged?.Invoke(_balance);
        yield return waiter;
        _balance += intermediateValue;
        BalanceChanged?.Invoke(_balance);
    }
}
