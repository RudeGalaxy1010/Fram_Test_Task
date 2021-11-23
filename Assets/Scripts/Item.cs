using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Custom/Item")]
[Serializable]
public class Item : ScriptableObject
{
    public string Name;
}
