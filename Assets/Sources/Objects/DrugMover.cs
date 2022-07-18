using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Drug))]
public class DrugMover : MonoBehaviour
{
    private const float Speed = 6f;
    private const float Offset = 1f;

    [SerializeField] private Transform _mountPoint;

    private Drug _drug;
    private Coroutine _coroutine;
    private Transform _startPoint;

    public Transform MountPoint => _mountPoint;

    private void Awake()
    {
        _drug = GetComponent<Drug>();
    }

    private void OnDestroy()
    {
        StopMove();
    }

    public void ChangeStartPoint(Transform startPosition)
    {
        _startPoint = startPosition;
    }

    public void PrepairPutIn(Transform finalPosition, Rigidbody rigidbody)
    {
        if (_coroutine != null)
            StopMove();

        _coroutine = StartCoroutine(PutIn(finalPosition, rigidbody));
    }

    private void StopMove()
    {
        StopCoroutine(_coroutine);
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

            if (transform.position == tempDirection && tempDirection != finalPosition.position)
                tempDirection = finalPosition.position;

            yield return null;
        }

        _drug.InizializeParameters(finalPosition, rigidbody);

        if (rigidbody.TryGetComponent(out DrugCollector drugCollector) == false)
            rigidbody.isKinematic = false;

        StopMove();
    }
}
