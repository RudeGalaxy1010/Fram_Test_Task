using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Targets : MonoBehaviour
{
    public UnityAction Complete;

    private List<Resource> _targets;
    private Inventory _inventory;

    public void Init(Inventory inventory, List<Resource> targets)
    {
        _inventory = inventory;
        _targets = targets;
        _inventory.Changed += (r) => UpdateTargets();
        UpdateTargets();
    }

    public void UpdateTargets()
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            if (!_inventory.Has(_targets[i]))
            {
                return;
            }
        }

        Complete?.Invoke();
    }

    private void OnDestroy()
    {
        _inventory.Changed -= (r) => UpdateTargets();
    }
}
