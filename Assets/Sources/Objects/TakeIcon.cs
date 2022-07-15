using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeIcon : MonoBehaviour
{
    [SerializeField] private Image _mainIcon;
    [SerializeField] private Image _fillIcon;

    private Coroutine _coroutine;
    private bool _isActive;
    private bool _isFilled;

    public event Action Complete;

    public void PrepairActivate(float issueTime)
    {
        if (_isActive && _isFilled)
        {
            _isFilled = false;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(Filling(issueTime));
        }
        else if (_isActive == false)
        {
            _isActive = true;
            _fillIcon.fillAmount = 0;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(Activate(issueTime));
        }
    }

    public void PrepairDeactivate()
    {
        if (gameObject.activeSelf == true)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(Deactivate());
        }
    }

    private void ChangeCoroutine(float issueTime)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Filling(issueTime));
    }

    private IEnumerator Activate(float issueTime)
    {
        WaitForSeconds waiter;
        Vector3 tempSize;
        float step;

        _mainIcon.gameObject.SetActive(true);
        _fillIcon.gameObject.SetActive(true);
        waiter = new WaitForSeconds(0.02f);
        tempSize = new Vector3(1, 1, 1);
        step = 0.1f;

        while (_fillIcon.rectTransform.localScale != tempSize)
        {
            _mainIcon.rectTransform.localScale = new Vector3(_mainIcon.rectTransform.localScale.x + step, _mainIcon.rectTransform.localScale.y + step, _mainIcon.rectTransform.localScale.z + step);
            _mainIcon.color = new Color(_mainIcon.color.r, _mainIcon.color.g, _mainIcon.color.b, _mainIcon.color.a + step);
            _fillIcon.rectTransform.localScale = new Vector3(_fillIcon.rectTransform.localScale.x + step, _fillIcon.rectTransform.localScale.y + step, _fillIcon.rectTransform.localScale.z + step);
            _fillIcon.color = new Color(_fillIcon.color.r, _fillIcon.color.g, _fillIcon.color.b, _fillIcon.color.a + step);
            yield return waiter;
        }

        ChangeCoroutine(issueTime);
    }

    private IEnumerator Deactivate()
    {
        WaitForSeconds waiter;
        Vector3 tempSize;
        float step;

        _fillIcon.fillAmount = 0;
        waiter = new WaitForSeconds(0.02f);
        tempSize = new Vector3(0, 0, 0);
        step = 0.1f;

        while (_fillIcon.rectTransform.localScale != tempSize)
        {
            _mainIcon.rectTransform.localScale = new Vector3(_mainIcon.rectTransform.localScale.x - step, _mainIcon.rectTransform.localScale.y - step, _mainIcon.rectTransform.localScale.z - step);
            _mainIcon.color = new Color(_mainIcon.color.r, _mainIcon.color.g, _mainIcon.color.b, _mainIcon.color.a - step);
            _fillIcon.rectTransform.localScale = new Vector3(_fillIcon.rectTransform.localScale.x - step, _fillIcon.rectTransform.localScale.y - step, _fillIcon.rectTransform.localScale.z - step);
            _fillIcon.color = new Color(_fillIcon.color.r, _fillIcon.color.g, _fillIcon.color.b, _fillIcon.color.a - step);
            yield return waiter;
        }

        _isActive = false;
        _mainIcon.gameObject.SetActive(false);
        _fillIcon.gameObject.SetActive(false);
    }

    private IEnumerator Filling(float issueTime)
    {
        WaitForSeconds waiter;
        float currentTime;

        waiter = new WaitForSeconds(0.01f);
        currentTime = 0;
        _fillIcon.fillAmount = 0;

        while (currentTime <= issueTime)
        {
            currentTime += Time.deltaTime;
            _fillIcon.fillAmount = currentTime / issueTime;
            yield return null;
        }

        _isFilled = true;
        Complete?.Invoke();
    }
}
