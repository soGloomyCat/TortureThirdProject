using UnityEngine;

public class Stalker : MonoBehaviour
{
    private const float Offset = 7;

    [SerializeField] private Mover _mover;

    private void OnEnable()
    {
        if (_mover == null)
            throw new System.ArgumentNullException("Отсутствует обязательный компонент. Проверьте инспектор.");
    }

    private void Update()
    {
        transform.position = new Vector3(_mover.gameObject.transform.position.x, transform.position.y, _mover.transform.position.z - Offset);
    }
}
