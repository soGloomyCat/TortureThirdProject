using UnityEngine;

public class Doctor : MonoBehaviour
{
    [SerializeField] private SickCollector _sickCollector;
    [SerializeField] private DrugCollector _drugCollector;

    public bool CheckCargoPresence()
    {
        if (_sickCollector.Fulness == 0 && _drugCollector.Fulness == 0)
            return true;

        return false;
    }
}
