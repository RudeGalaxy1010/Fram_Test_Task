using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Prefab Storage", menuName = "Custom/PrefabStorage")]
public class PrefabStorage : ScriptableObject
{
    [SerializeField] private List<CropSettings> _crops;

    public IReadOnlyList<CropSettings> Crops => _crops;
}
