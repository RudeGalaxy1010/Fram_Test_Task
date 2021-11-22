using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Custom/Level Data")]
public class LevelData : ScriptableObject
{
    public Vector2Int FieldSize;
    public TargetSettings Targets;
}
