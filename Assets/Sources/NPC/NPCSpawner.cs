using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private List<SickCharacter> _sickCharacters;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Bench _bench;
    [SerializeField] private SpecialNPC _specialNPC;
    [Range(1, 10)]
    [SerializeField] private float _cooldownBetweenSpawn;

    private float _currentTime;
    private int _wayIndex;
    private int _spawnedCount;

    public event Action<SickCharacter> SpawnedNewSick;

    private void Start()
    {
        _currentTime = _cooldownBetweenSpawn;
    }

    private void Update()
    {
        if (_bench.CheckPresenceFreeSeat() && _currentTime >= _cooldownBetweenSpawn)
            Spawn();

        _currentTime += Time.deltaTime;
    }

    private void Spawn()
    {
        SickCharacter tempSick;
        int sickCharacterIndex;

        _currentTime = 0;
        _wayIndex = UnityEngine.Random.Range(0, _spawnPoints.Count);
        sickCharacterIndex = UnityEngine.Random.Range(0, _sickCharacters.Count);

        if (_spawnedCount % 4 == 0)
            tempSick = Instantiate(_specialNPC, _spawnPoints[_wayIndex]);
        else
            tempSick = Instantiate(_sickCharacters[sickCharacterIndex], _spawnPoints[_wayIndex]);

        tempSick.InizializeParameters(_bench.TakeFreeSeat(tempSick), _spawnPoints[_wayIndex]);
        SpawnedNewSick?.Invoke(tempSick);
        _spawnedCount++;
    }
}
