using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterJoint))]
[RequireComponent(typeof(Rigidbody))]
public class Drug : MonoBehaviour
{
    private const float Speed = 6f;
    private const float Offset = 1;

    [SerializeField] private Transform _mountPoint;
    [SerializeField] private string _label;

    private CharacterJoint _joint;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;
    private Transform _startPoint;
    private bool _isUsed;

    public Transform MountPoint => _mountPoint;
    public Rigidbody Rigidbody => _rigidbody;
    public bool IsUsed => _isUsed;
    public string Label => _label;

    public void ChangeStartPoint(Transform startPosition)
    {
        _startPoint = startPosition;
    }

    public void PrepairPutIn(Transform finalPosition, Rigidbody rigidbody)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(PutIn(finalPosition, rigidbody));
    }

    public void InizializeParameters(Transform MountPosition, Rigidbody connectedBody)
    {
        transform.position = MountPosition.position;
        _joint.connectedBody = connectedBody;
        _rigidbody.isKinematic = false;
    }

    public void Use()
    {
        _isUsed = true;
    }

    public void Eject()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        _joint = GetComponent<CharacterJoint>();
        _rigidbody = GetComponent<Rigidbody>();
        _isUsed = false;
    }

    private IEnumerator PutIn(Transform finalPosition, Rigidbody rigidbody)
    {
        Vector3 tempDirection;

        rigidbody.isKinematic = true;
        transform.position = _startPoint.position;
        tempDirection = new Vector3(transform.position.x, finalPosition.position.y - Offset, transform.position.z);

        while (transform.position != tempDirection)
        {
            transform.position = Vector3.MoveTowards(transform.position, tempDirection, Speed * Time.deltaTime);
            yield return null;
        }

        while (transform.position != finalPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPosition.position, Speed * Time.deltaTime);
            yield return null;
        }

        InizializeParameters(finalPosition, rigidbody);

        if (rigidbody.TryGetComponent(out DrugCollector drugCollector) == false)
            rigidbody.isKinematic = false;
    }
}
