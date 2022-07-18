using UnityEngine;

[RequireComponent(typeof(DoctorAnimationHandler))]
public class Mover : MonoBehaviour
{
    [Range(1, 3)]
    [SerializeField] private float _walkSpeed;
    [Range(1, 15)]
    [SerializeField] private float _rotateSpeed;

    private DoctorAnimationHandler _animationHandler;

    private void Awake()
    {
        _animationHandler = GetComponent<DoctorAnimationHandler>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(Vector3.forward, Quaternion.Euler(0, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector3.left, Quaternion.Euler(0, -90, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector3.back, Quaternion.Euler(0, 180, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(Vector3.right, Quaternion.Euler(0, 90, 0));
        }

        if (Input.anyKey)
            _animationHandler.EnableMoveAnimation();
        if (Input.anyKey == false)
            _animationHandler.DisableMoveAnimation();
    }

    private void Move(Vector3 moveDirection, Quaternion rotateDirection)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, _walkSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotateDirection, _rotateSpeed * Time.deltaTime);
    }
}
