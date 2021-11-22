using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetsPanel : MonoBehaviour
{
    [SerializeField] private Text _eggsCount;
    [SerializeField] private Text _milkCount;

    public void Init(TargetSettings targets)
    {
        _eggsCount.text = targets.Resources.FirstOrDefault(r => r.Item.Name == "Egg").Quantity.ToString();
        _milkCount.text = targets.Resources.FirstOrDefault(r => r.Item.Name == "Milk").Quantity.ToString();
    }
}
