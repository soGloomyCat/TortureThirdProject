using UnityEngine;

public class CureTransition : Transition
{
    private void OnTriggerEnter(Collider interactiveObject)
    {
        if (interactiveObject.TryGetComponent(out Bed bed))
            NeedTransit = true;
    }
}
