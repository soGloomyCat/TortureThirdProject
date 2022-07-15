using UnityEngine;

public class CureState : State
{
    private const string AnimationTrigger = "IsLieDown";

    private void Start()
    {
        Animator.SetBool(AnimationTrigger, true);
        SickCharacter.ShowDisease();
    }
}
