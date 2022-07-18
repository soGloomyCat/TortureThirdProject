using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Reward : MonoBehaviour
{
    private const float SphereRadius = .15f;
    private const float Speed = 4f;
    private const float YOffset = 1.5f;
    private const float ZOffset = 0.5f;

    [Range(0.1f, 10)]
    [SerializeField] private float _lifeTime;

    private float _currentTime;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    private void Awake()
    {
        _currentTime = 0;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_currentTime >= _lifeTime)
            Destroy(gameObject);

        _currentTime += Time.deltaTime;
    }

    public void Dump()
    {
        _rigidbody.AddForce(Random.insideUnitSphere * SphereRadius, ForceMode.Impulse);
    }

    public void PrepairToss()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Toss());
    }

    private IEnumerator Toss()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + ZOffset);
        Vector3 startPosition = transform.position;
        Vector3 finalPosition = new Vector3(startPosition.x, startPosition.y + YOffset, startPosition.z);


        while (transform.position != finalPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, Speed * Time.deltaTime);

            if (transform.position == finalPosition)
                finalPosition = startPosition;

            yield return null;
        }
    }
}
