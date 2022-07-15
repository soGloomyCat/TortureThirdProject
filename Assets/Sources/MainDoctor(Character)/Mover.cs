using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Mover : MonoBehaviour
{
    private const string AnimationLabel = "IsWalk";

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _rotateSpeed;

    private Animator _animator;
    private bool _isWalk;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _isWalk = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.forward, _walkSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), _rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, _walkSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -90, 0), _rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.back, _walkSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), _rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right, _walkSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), _rotateSpeed * Time.deltaTime);
        }

        if (Input.anyKey)
            ActivateAnimation();
        if (Input.anyKey == false)
            DeactivateAnimation();
    }

    private void ActivateAnimation()
    {
        if (_isWalk == false)
        {
            _isWalk = true;
            _animator.SetBool(AnimationLabel, true);
        }
    }

    private void DeactivateAnimation()
    {
        if (_isWalk == true)
        {
            _isWalk = false;
            _animator.SetBool(AnimationLabel, false);
        }
    }
}
