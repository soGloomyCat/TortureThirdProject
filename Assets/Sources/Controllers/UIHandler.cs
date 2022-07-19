using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Image _dropIcon;
    [SerializeField] private TMP_Text _balanceText;

    private int _currentBalance;
    private int _tempBalance;
    private Coroutine _coroutine;

    public void EnableIcon()
    {
        _dropIcon.gameObject.SetActive(true);
    }

    public void DisableIcon()
    {
        _dropIcon.gameObject.SetActive(false);
    }

    public void ChangeBalanceText(int balance)
    {
        _tempBalance = balance;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeBalanceText());
    }

    private IEnumerator ChangeBalanceText()
    {
        WaitForSeconds waiter;

        waiter = new WaitForSeconds(0.1f);

        while (_currentBalance != _tempBalance)
        {
            _currentBalance++;
            _balanceText.text = _currentBalance.ToString();
            yield return waiter;
        }
    }
}
