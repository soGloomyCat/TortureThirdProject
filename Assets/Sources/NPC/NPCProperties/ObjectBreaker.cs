using UnityEngine;

public class ObjectBreaker : MonoBehaviour
{
    private const float YOffset = 0.75f;

    [SerializeField] private SickCharacter _sick;
    [SerializeField] private Flasher _flasher;
    [SerializeField] private Transform _flasherPoint;

    private void OnEnable()
    {
        _sick.Issued += TurnOff;
    }

    private void OnDisable()
    {
        _sick.Issued -= TurnOff;
    }

    private void TurnOff()
    {
        _flasher.gameObject.SetActive(false);
    }

    private void Update()
    {
        CorrectFlasherPosition();
    }

    private void CorrectFlasherPosition()
    {
        _flasher.transform.position = new Vector3(_flasherPoint.position.x, _flasherPoint.position.y + YOffset, _flasherPoint.position.z);
    }
}
