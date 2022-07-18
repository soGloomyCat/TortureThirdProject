using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NPCAnimationHandler : MonoBehaviour
{
    private const string FirstAnimationTrigger = "IsSitDown";
    private const string SecondAnimationTrigger = "IsHang";
    private const string ThirdAnimationTrigger = "IsLieDown";
    private const string FourthAnimationTrigger = "IsLeave";

    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void SitDown()
    {
        _animator.SetBool(FirstAnimationTrigger, true);
    }

    public void HangOn()
    {
        _animator.SetBool(SecondAnimationTrigger, true);
    }

    public void LieDown()
    {
        _animator.SetBool(ThirdAnimationTrigger, true);
    }

    public void LeaveOut()
    {
        _animator.SetBool(FourthAnimationTrigger, true);
    }
}
