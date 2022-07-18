using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterJoint))]
[RequireComponent(typeof(Rigidbody))]
public class DrugHolder : MonoBehaviour
{
    private CharacterJoint _joint;
    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _joint = GetComponent<CharacterJoint>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void InizializeParameters(Rigidbody connectedBody)
    {
        _joint.connectedBody = connectedBody;
        _rigidbody.isKinematic = false;
    }
}
