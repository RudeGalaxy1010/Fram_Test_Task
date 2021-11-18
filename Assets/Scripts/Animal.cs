using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Animal : MonoBehaviour
{
    public UnityAction<float> Updated;

    public int SettingsId { get; private set; } = -1;
    public float ProductionTime { get; private set; }
    public float ProductionTimer { get; private set; }
    public ConsumationResource Requirement { get; set; }
    public Resource Input { get; private set; }
    public Resource Output { get; private set; }

    private float _resourceConsumingTime;

    public float Progress => Mathf.Min(ProductionTimer / ProductionTime, 1f);

    public void Init(AnimalSettings settings)
    {
        SettingsId = settings.Id;
        ProductionTime = settings.ProductionTime;
        ProductionTimer = 0;
        Requirement = settings.Requirement;
        Output = new Resource(settings.Output);
        _resourceConsumingTime = 0;
    }

    public void AddProgress(float value)
    {
        if (Progress == 1)
        {
            return;
        }

        if (Input.Quantity < Requirement.Resource.Quantity)
        {
            return;
        }

        if (_resourceConsumingTime >= Requirement.ConsumingTime)
        {
            Input = new Resource(Input.Item, Input.Quantity - Requirement.Resource.Quantity);
            _resourceConsumingTime = 0;
        }

        ProductionTimer += value;
        _resourceConsumingTime += value;
        Updated?.Invoke(Progress);
    }

    public Resource Collect()
    {
        if (Progress != 1)
        {
            throw new Exception("Can't collect resource");
        }

        ProductionTimer = 0;
        return Output;
    }
}
