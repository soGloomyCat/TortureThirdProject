using UnityEngine;

public class HangTransition : Transition
{
    private const float Offset = 1.5f;

    [SerializeField] private Transform _finalRayPoint;
    [SerializeField] private float _hangTime;
    [SerializeField] private SkinnedMeshRenderer _renderer;

    private TakeIcon _takeIcon;
    private bool _isDoctorHang;

    private void ChangeTransitStatus()
    {
        NeedTransit = true;
        Destroy(_takeIcon.gameObject);
    }

    private void Start()
    {
        _takeIcon = Instantiate(SickCharacter.TakeIcon);
        _takeIcon.Complete += HangOn;
        _isDoctorHang = false;
    }

    private void FixedUpdate()
    {
        DetermineDoctor();
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
            if (tempHit.transform.TryGetComponent(out SickCollector sickCollector))
            {
                if (_isDoctorHang == false)
                {
                    _isDoctorHang = true;
                    _takeIcon.transform.position = new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z);
                    _takeIcon.PrepairActivate(_hangTime);
                }

                return;
            }
        }
        else
        {
            if (NeedTransit == false)
            {
                _takeIcon.PrepairDeactivate();
                _isDoctorHang = false;
            }
        }
    }

    private void HangOn()
    {
        _takeIcon.Complete -= HangOn;
        ChangeTransitStatus();
        SickCharacter.HangOn();
    }
}
