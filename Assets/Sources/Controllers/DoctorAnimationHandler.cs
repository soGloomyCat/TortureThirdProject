using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoctorAnimationHandler : MonoBehaviour
{
    private const string AnimationCargoTrigger = "WithCargo";
    private const string AnimationMoveTrigger = "IsWalk";

    private Animator _animator;

    private void OnEnable() => _animator = GetComponent<Animator>();

    public void EnableHangAnimation() => _animator.SetBool(AnimationCargoTrigger, true);

    public void EnableMoveAnimation() => _animator.SetBool(AnimationMoveTrigger, true);

    public void DisableHangAnimation() => _animator.SetBool(AnimationCargoTrigger, false);

    public void DisableMoveAnimation() => _animator.SetBool(AnimationMoveTrigger, false);
}
