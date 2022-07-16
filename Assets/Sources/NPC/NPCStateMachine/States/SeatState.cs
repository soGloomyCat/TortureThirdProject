using System.Collections;
using UnityEngine;

public class SeatState : State
{
    private const string AnimationTrigger = "IsSitDown";
    private const float Speed = 2f;
    private const float Offset = 0.3f;

    private Transform _seatPosition;
    private Coroutine _coroutine;

    private void Start()
    {
        _seatPosition = SickCharacter.SeatPosition;
        PrepairSitDown();
    }

    private void PrepairSitDown()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SitDown());
    }

    private IEnumerator SitDown()
    {
        Vector3 tempSeatPosition;
        Vector3 tempRotationDirection;
        Quaternion tempRotation;

        tempSeatPosition = new Vector3(_seatPosition.position.x, transform.position.y, _seatPosition.position.z);

        while (transform.position != tempSeatPosition)
        {
            tempRotationDirection = tempSeatPosition - transform.position;
            tempRotation = Quaternion.LookRotation(tempRotationDirection, Vector3.up);
            transform.rotation = tempRotation;
            transform.position = Vector3.MoveTowards(transform.position, tempSeatPosition, Speed * Time.deltaTime);

            yield return null;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
        Animator.SetBool(AnimationTrigger, true);
        transform.position = new Vector3(_seatPosition.position.x, _seatPosition.position.y - Offset, _seatPosition.position.z);
    }
}
