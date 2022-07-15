using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Image dropIcon;
    [SerializeField] private TMP_Text _balanceText;

    public void EnableIcon() => dropIcon.gameObject.SetActive(true);

    public void DisableIcon() => dropIcon.gameObject.SetActive(false);

    public void ChangeBalanceText(int balance) => _balanceText.text = balance.ToString();
}
