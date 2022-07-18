using UnityEngine;

public class Doctor : MonoBehaviour
{
    [SerializeField] private SickPoolHandler _sickCollector;
    [SerializeField] private DrugPoolHandler _drugCollector;

    public bool HasCargo()
    {
        if (_sickCollector.Fulness > 0 || _drugCollector.Fulness > 0)
            return true;

        return false;
    }
}
