using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Prefab Storage", menuName = "Custom/PrefabStorage")]
public class PrefabStorage : ScriptableObject
{
    [SerializeField] private List<CropSettings> _crops;
    [SerializeField] private List<AnimalSettings> _animals;

    public IReadOnlyList<CropSettings> Crops => _crops;
    public IReadOnlyList<AnimalSettings> Animals => _animals;
}
