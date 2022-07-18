using System.Collections.Generic;
using UnityEngine;

public class SickPoolHandler : MonoBehaviour
{
    private const int TriggerValue = 1;
    private const float Offset = 0.15f;
    private const float YRotationAngle = -82f;

    [SerializeField] private Transform _sickPool;

    private List<SickCharacter> _sickCharacters;
    private SickCharacter _lastsick;
    private int _pickedSickCount;

    public int Fulness => _sickCharacters.Count;

    private void Awake()
    {
        _sickCharacters = new List<SickCharacter>();
        _pickedSickCount = 0;
    }

    public SickCharacter GetLastSick()
    {
        SickCharacter tempSick = _lastsick;

        _sickCharacters.RemoveAt(_sickCharacters.Count - TriggerValue);

        if (_sickCharacters.Count > 0)
            _lastsick = _sickCharacters[_sickCharacters.Count - TriggerValue];

        _pickedSickCount = _sickCharacters.Count;
        return tempSick;
    }

    public void ClearPool()
    {
        foreach (var item in _sickCharacters)
            Destroy(item.gameObject);

        _sickCharacters.Clear();
        _pickedSickCount = 0;
    }

    public void HangOnSick(SickCharacter sick)
    {
        _lastsick = sick;
        _lastsick.gameObject.transform.parent = _sickPool;
        _lastsick.gameObject.transform.rotation = _sickPool.rotation;
        _sickCharacters.Add(_lastsick);

        if (_sickPool.childCount <= TriggerValue)
        {
            _sickCharacters[_pickedSickCount].gameObject.transform.position = _sickPool.position;
        }
        else
        {
            _sickCharacters[_pickedSickCount].gameObject.transform.position = new Vector3(_sickPool.transform.position.x,
                                                                                          _sickPool.transform.position.y + Offset * _pickedSickCount,
                                                                                          _sickPool.transform.position.z);
        }

        _sickCharacters[_pickedSickCount++].transform.localRotation = Quaternion.Euler(0, YRotationAngle, 0);
    }
}
