using UnityEngine;

public class HangState : State
{
    private const string AnimationTrigger = "IsHang";

    private void Start() => Animator.SetBool(AnimationTrigger, true);
}
