using System.Collections;
using UnityEngine;

public class Drug : MonoBehaviour
{
    [SerializeField] private string _label;

    private DrugMover _drugMover;
    private DrugHolder _drugHolder;
    private bool _isUsed;

    public bool IsUsed => _isUsed;
    public string Label => _label;

    private void Awake()
    {
        _drugMover = GetComponent<DrugMover>();
        _drugHolder = GetComponent<DrugHolder>();
        _isUsed = false;
    }

    public void ChangeStartPoint(Transform startPosition)
    {
        _drugMover.ChangeStartPoint(startPosition);
    }

    public void PrepairPutIn(Transform finalPosition, Rigidbody rigidbody)
    {
        _drugMover.PrepairPutIn(finalPosition, rigidbody);
    }

    public void InizializeParameters(Transform MountPosition, Rigidbody connectedBody)
    {
        transform.position = MountPosition.position;
        _drugHolder.InizializeParameters(connectedBody);
    }

    public void Use()
    {
        _isUsed = true;
    }

    public void Eject()
    {
        Destroy(gameObject);
    }

    public Transform GetMountPoint()
    {
        return _drugMover.MountPoint;
    }

    public Rigidbody GetConnectedBody()
    {
        return _drugHolder.Rigidbody;
    }
}
