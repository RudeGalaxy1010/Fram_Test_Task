using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Target settings", menuName = "Custom/Target Settings")]
public class TargetSettings : ScriptableObject
{
    public List<Resource> Resources;
}
