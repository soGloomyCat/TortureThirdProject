using UnityEngine;

[RequireComponent(typeof(DoctorAnimationHandler))]
public class Mover : MonoBehaviour
{
    [Range(1, 3)]
    [SerializeField] private float _walkSpeed;
    [Range(1, 15)]
    [SerializeField] private float _rotateSpeed;

    private DoctorAnimationHandler _animationHandler;
    private Quaternion _rotateDirection;

    private void Awake()
    {
        _animationHandler = GetComponent<DoctorAnimationHandler>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Move();
            _animationHandler.EnableMoveAnimation();
        }
        if (Input.anyKey == false)
        {
            _animationHandler.DisableMoveAnimation();
        }
    }

    private void Move()
    {
        var horizontal = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
        var vertical = Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0;
        var direction = new Vector3(horizontal, 0, vertical);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _walkSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position + direction * _rotateSpeed), _rotateSpeed * Time.deltaTime);
    }
}
