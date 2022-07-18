using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DrugCollector))]
public class DrugPoolHandler : MonoBehaviour
{
    private const int TriggerValue = 1;

    [SerializeField] private Transform _drugPool;

    private DrugCollector _collector;
    private List<Drug> _drugs;
    private Transform _spawnPoint;
    private Drug _lastDrug;

    public int Fulness => _drugs.Count;
    public List<Drug> Drugs => _drugs;

    private void Awake()
    {
        _collector = GetComponent<DrugCollector>();
        _drugs = new List<Drug>();
    }

    public void AcceptDrug(Drug drug)
    {
        Drug tempDrug = Instantiate(drug, _drugPool);

        tempDrug.ChangeStartPoint(_spawnPoint);

        if (_drugPool.childCount == TriggerValue)
            Inizialize(tempDrug, _drugPool, _collector.Rigidbody);
        else
            Inizialize(tempDrug, _lastDrug.GetMountPoint(), _lastDrug.GetConnectedBody());
    }

    public void OverrideDrugsposition()
    {
        foreach (var drug in _drugs)
        {
            if (drug.IsUsed == true)
            {
                drug.Eject();
                _drugs.Remove(drug);
                break;
            }
        }

        for (int i = _drugs.Count - TriggerValue; i >= 0; i--)
        {
            if (i > 0)
                _drugs[i].InizializeParameters(_drugs[i - TriggerValue].GetMountPoint(), _drugs[i - TriggerValue].GetConnectedBody());
            else
                _drugs[i].InizializeParameters(_drugPool, _collector.Rigidbody);
        }
    }

    public void ClearPool()
    {
        foreach (var item in _drugs)
            Destroy(item.gameObject);

        _drugs.Clear();
    }

    public void SetDrugSpawnPoint(Transform spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

    private void Inizialize(Drug drug, Transform pool, Rigidbody rigidbody)
    {
        drug.PrepairPutIn(pool, rigidbody);
        _lastDrug = drug;
        _drugs.Add(drug);
    }
}
