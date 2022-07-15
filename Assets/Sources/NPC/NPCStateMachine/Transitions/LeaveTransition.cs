public class LeaveTransition : Transition
{
    private void Update()
    {
        if (SickCharacter.IsDrugFounded)
            NeedTransit = true;
    }
}
