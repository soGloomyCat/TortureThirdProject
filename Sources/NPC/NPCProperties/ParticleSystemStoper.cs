using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemStoper : MonoBehaviour
{
    [SerializeField] private SickCharacter _sick;
    [SerializeField] private List<ParticleSystem> _turnedOffParticle;

    private void OnEnable()
    {
        _sick.Issued += TurnOff;
    }

    private void OnDisable()
    {
        _sick.Issued -= TurnOff;
    }

    private void TurnOff()
    {
        foreach (var particleSystem in _turnedOffParticle)
        {
            particleSystem.Stop();
            particleSystem.Clear();
        }
    }
}
