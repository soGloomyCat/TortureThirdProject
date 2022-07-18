using System;
using System.Collections;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _balance;
    private int _tempBalance;
    private Coroutine _coroutine;

    public event Action<int> BalanceChanged;

    private void Start()
    {
        _balance = 0;
        BalanceChanged?.Invoke(_balance);
    }

    public void PrepairChange(int reward)
    {
        _tempBalance += reward;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeBalance());
    }

    private IEnumerator ChangeBalance()
    {
        WaitForSeconds waiter;

        waiter = new WaitForSeconds(0.1f);

        while (_balance != _tempBalance)
        {
            _balance++;
            BalanceChanged?.Invoke(_balance);
            yield return waiter;
        }
    }
}
