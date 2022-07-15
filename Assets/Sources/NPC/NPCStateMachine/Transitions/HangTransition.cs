using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HangTransition : Transition
{
    private const float Multiplier = 40;

    [SerializeField] private Transform _finalRayPoint;
    [SerializeField] private float _hangTime;
    [SerializeField] private SkinnedMeshRenderer _renderer;

    private TakeIcon _originalTakeIcon;
    private TakeIcon _tempTakeIcon;
    private bool _isDoctorHang;

    private void ChangeTransitStatus()
    {
        NeedTransit = true;
        Destroy(_tempTakeIcon.gameObject);
    }

    private void Start()
    {
        _originalTakeIcon = SickCharacter.TakeIcon;
        _isDoctorHang = false;

        if (_tempTakeIcon == null)
        {
            _tempTakeIcon = Instantiate(_originalTakeIcon, _originalTakeIcon.transform.parent);
            _tempTakeIcon.Complete += HangOn;
        }
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
                    _tempTakeIcon.FillIcon.rectTransform.localPosition = _tempTakeIcon.FillIcon.rectTransform.TransformPoint(transform.position) * Multiplier;
                    _tempTakeIcon.gameObject.SetActive(true);
                    _tempTakeIcon.PrepairActivate(_hangTime);
                }

                return;
            }
        }
        else
        {
            if (NeedTransit == false)
            {
                _tempTakeIcon.PrepairDeactivate();
                _isDoctorHang = false;
            }
        }
    }

    private void HangOn()
    {
        _tempTakeIcon.Complete -= HangOn;
        ChangeTransitStatus();
        SickCharacter.HangOn();
    }
}
