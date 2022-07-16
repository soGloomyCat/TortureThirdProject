using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveState : State
{
    private const string AnimationTrigger = "IsLeave";
    private const float Offset = 0.5f;

    [Range(1, 5)]
    [SerializeField] private float _speed;

    private List<Transform> _wayPoints;
    private Coroutine _coroutine;

    private void Start()
    {
        _wayPoints = SickCharacter.ExitWayPoints;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Move());
    }

    private void Leave()
    {
        StopCoroutine(_coroutine);
        Destroy(gameObject);
    }

    private IEnumerator Move()
    {
        int pointIndex;
        Vector3 tempMoveDirection;
        Vector3 tempRotationDirection;
        Quaternion tempRotation;

        pointIndex = 0;
        transform.position = new Vector3(transform.position.x, _wayPoints[pointIndex].position.y, transform.position.z + Offset);
        tempMoveDirection = new Vector3(_wayPoints[pointIndex].position.x, transform.position.y, _wayPoints[pointIndex].position.z);
        Animator.SetBool(AnimationTrigger, true);

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
                    Leave();
                }
            }

            yield return null;
        }
    }
}
