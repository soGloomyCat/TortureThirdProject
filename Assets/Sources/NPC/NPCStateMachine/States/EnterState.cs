using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterState : State
{
    [Range(1, 5)]
    [SerializeField] private float _speed;

    private List<Transform> _wayPoints;
    private Coroutine _coroutine;

    public event Action Reached;

    private void Start()
    {
        _wayPoints = SickCharacter.WayPoints;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        int pointIndex;
        Vector3 tempMoveDirection;
        Vector3 tempRotationDirection;
        Quaternion tempRotation;

        pointIndex = 0;
        tempMoveDirection = new Vector3(_wayPoints[pointIndex].position.x, transform.position.y, _wayPoints[pointIndex].position.z);

        while (transform.position != tempMoveDirection)
        {
            tempRotationDirection = tempMoveDirection - transform.position;
            tempRotation = Quaternion.LookRotation(tempRotationDirection, Vector3.up);
            transform.rotation = tempRotation;
            transform.position = Vector3.MoveTowards(transform.position, tempMoveDirection, _speed * Time.deltaTime);

            if (transform.position == tempMoveDirection)
            {
                pointIndex++;

                if (pointIndex < _wayPoints.Count)
                {
                    tempMoveDirection = new Vector3(_wayPoints[pointIndex].position.x, transform.position.y, _wayPoints[pointIndex].position.z);
                }
                else
                {
                    Reached?.Invoke();
                    break;
                }
            }

            yield return null;
        }
    }
}
