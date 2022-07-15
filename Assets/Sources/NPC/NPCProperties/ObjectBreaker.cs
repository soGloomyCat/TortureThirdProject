using UnityEngine;

public class ObjectBreaker : MonoBehaviour
{
    [SerializeField] private SickCharacter _sick;
    [SerializeField] private Flasher _turnedOffObject;
    [SerializeField] private Transform _flasherPoint;

    private Vector3 _offset;

    private void OnEnable()
    {
        _sick.Issued += TurnOff;
        _offset = new Vector3(0, .75f, 0);
    }

    private void OnDisable()
    {
        _sick.Issued -= TurnOff;
    }

    private void TurnOff()
    {
        _turnedOffObject.gameObject.SetActive(false);
    }

    private void Update()
    {
        _turnedOffObject.transform.position = _flasherPoint.position + _offset;
    }
}
