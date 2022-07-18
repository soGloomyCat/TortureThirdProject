using UnityEngine;

public class TakeIconAnimator : MonoBehaviour
{
    private const string FirstAnimationTrigger = "Deactivate";
    private const string SecondAnimationTrigger = "Activate";

    [SerializeField] private Animator _animator;

    public void Activate()
    {
        _animator.SetBool(FirstAnimationTrigger, false);
        _animator.SetBool(SecondAnimationTrigger, true);
    }

    public void Deactivate()
    {
        _animator.SetBool(FirstAnimationTrigger, true);
        _animator.SetBool(SecondAnimationTrigger, false);
    }
}
