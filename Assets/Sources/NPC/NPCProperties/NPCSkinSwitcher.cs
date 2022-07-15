using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SickCharacter))]
public class NPCSkinSwitcher : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _currentMesh;
    [SerializeField] private Mesh _newMesh;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _newMaterial;

    private SickCharacter _sickCharacter;

    private void Start()
    {
        _sickCharacter = GetComponent<SickCharacter>();
        _sickCharacter.Issued += ChangeMesh;
    }

    private void ChangeMesh()
    {
        List<Material> tempList;

        tempList = new List<Material>();
        _currentMesh.sharedMesh = _newMesh;
        _currentMesh.GetMaterials(tempList);

        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i] = _newMaterial;
        }

        _sickCharacter.Issued -= ChangeMesh;
    }
}
