using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Settings", menuName = "Custom/Animal Settings")]
public class AnimalSettings : ScriptableObject
{
    public int Id;
    public string Name;
    public float ProductionTime;
    public ConsumableResource Input;
    public Resource Output;
    public View Prefab;
}
