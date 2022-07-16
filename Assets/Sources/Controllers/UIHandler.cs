using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Image _dropIcon;
    [SerializeField] private TMP_Text _balanceText;

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
        _balanceText.text = balance.ToString();
    }

    private void OnEnable()
    {
        if (_dropIcon == null || _balanceText == null)
            throw new System.ArgumentNullException("Отсутствует обязательный компонент. Проверьте инспектор.");
    }
}
