using UnityEngine;

public class ObjectBreaker : MonoBehaviour
{
    [SerializeField] private SickCharacter _sick;
    [SerializeField] private Flasher _flasher;
    [SerializeField] private Transform _flasherPoint;

    private Vector3 _offset;

    private void OnEnable()
    {
        if (_sick == null || _flasher == null || _flasherPoint == null)
            throw new System.ArgumentNullException("Отсутствует обязательный компонент. Проверьте инспектор.");

        _sick.Issued += TurnOff;
        _offset = new Vector3(0, .75f, 0);
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
        _flasher.transform.position = _flasherPoint.position + _offset;
    }
}
