using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop Settings", menuName = "Custom/Crop Settings")]
public class CropSettings : ScriptableObject
{
    public int Id;
    public string Name;
    public float GrowTime;
    public Resource Output;
    public GameObject Prefab;
}
