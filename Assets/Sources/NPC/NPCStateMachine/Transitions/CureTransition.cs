public class CureTransition : Transition
{
    private void Start()
    {
        SickCharacter.ReadyForCure += ChangeTransitStatus;
    }

    public void ChangeTransitStatus()
    {
        SickCharacter.ReadyForCure -= ChangeTransitStatus;
        NeedTransit = true;
    }
}
