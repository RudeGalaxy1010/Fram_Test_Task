using Farm.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "new PrefabsStorage", menuName = "Prefabs Storage")]
public class PrefabsStorage : ScriptableObject
{
    [Header("Plants")]
    public CropSettings Wheat;

    //[Header("Animals")]
    //public GameObject Cow;
    //public GameObject Chicken;
}
