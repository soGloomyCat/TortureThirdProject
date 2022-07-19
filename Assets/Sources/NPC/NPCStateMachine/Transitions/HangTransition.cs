using UnityEngine;

public class HangTransition : Transition
{
    private const float Offset = 1.5f;

    [Range(0.1f, 5)]
    [SerializeField] private float _hangTime;
    [SerializeField] private Transform _finalRayPoint;
    [SerializeField] private TakeIcon _takeIcon;

    private Timer _timer;
    private bool _isDoctorHang;

    private void Start()
    {
        _timer = new Timer();
        _takeIcon = Instantiate(_takeIcon);
        _takeIcon.Init(_timer);
        _takeIcon.Completed += HangOn;
        _isDoctorHang = false;
    }

    private void FixedUpdate()
    {
        DetermineDoctor();

        if (_isDoctorHang)
            _timer.Tick(Time.deltaTime);
        else
            _timer.Stop();

    }

    private void ChangeTransitStatus()
    {
        Destroy(_takeIcon.gameObject);
        NeedTransit = true;
    }

    private void DetermineDoctor()
    {
        Vector3 tempDirection;
        Ray tempRay;
        RaycastHit tempHit;
        float rayLengt;

        tempDirection = _finalRayPoint.position - transform.position;
        tempRay = new Ray(transform.position, tempDirection);
        rayLengt = 1.5f;

        if (Physics.Raycast(tempRay, out tempHit, rayLengt))
        {
            if (tempHit.transform.TryGetComponent(out SickCollector sickCollector) && _isDoctorHang == false)
            {
                _isDoctorHang = true;
                _takeIcon.transform.position = new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z);
                _timer.StartCountdown(_hangTime);
            }
        }
        else
        {
            _isDoctorHang = false;
        }
    }

    private void HangOn()
    {
        _takeIcon.Completed -= HangOn;
        ChangeTransitStatus();
        SickCharacter.HangOn();
    }
}
