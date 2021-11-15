using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop Settings", menuName = "Custom/Crop Settings")]
public class CropSettings : ScriptableObject
{
    public string Name;
    public float GrowTime;
    public Resource Requirement;
    public Resource Output;
    public GameObject Prefab;
}
