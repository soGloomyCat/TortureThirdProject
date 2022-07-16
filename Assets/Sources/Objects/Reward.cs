using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Reward : MonoBehaviour
{
    private const float SphereRadius = .15f;
    private const float Speed = 4f;
    private const float Offset = 1.5f;

    [Range(0.1f, 10)]
    [SerializeField] private float _lifeTime;

    private float _currentTime;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

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

    private void OnEnable()
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

    private IEnumerator Toss()
    {
        Vector3 rewardPosition;
        Vector3 startPosition;
        Vector3 finalPosition;

        rewardPosition = new Vector3(0.03f, 0.03f, 0.44f);
        transform.localPosition = rewardPosition;
        startPosition = transform.position;
        finalPosition = new Vector3(startPosition.x, startPosition.y + Offset, startPosition.z);

        while (transform.position != finalPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, Speed * Time.deltaTime);
            yield return null;
        }

        while (transform.position != startPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, Speed * Time.deltaTime);
            yield return null;
        }
    }
}
