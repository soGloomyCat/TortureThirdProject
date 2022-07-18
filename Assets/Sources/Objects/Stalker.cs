using UnityEngine;

public class Stalker : MonoBehaviour
{
    private const float Offset = 7.5f;

    [SerializeField] private Mover _mover;

    private void Update()
    {
        transform.position = new Vector3(_mover.gameObject.transform.position.x, transform.position.y, _mover.transform.position.z - Offset);
    }
}
