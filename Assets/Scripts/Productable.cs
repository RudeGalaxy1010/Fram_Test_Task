using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Productable
{
    public UnityAction<float> Updated;

    public float ProductionTime { get; private set; }
    public float ProductionTimer { get; private set; }
    public Resource Output { get; private set; }
    public Resource OutputStorage { get; private set; }

    public float Progress => Mathf.Min(ProductionTimer / ProductionTime, 1f);

    public Productable(float productionTime, Resource output)
    {
        ProductionTime = productionTime;
        Output = output;

        ProductionTimer = 0;
        OutputStorage = new Resource(output.Item, 0);
    }

    public void Update(float value)
    {
        AddProgress(value);
        Updated?.Invoke(Progress);
    }

    protected virtual void AddProgress(float value)
    {
        if (!CanProduct())
        {
            return;
        }

        ProductionTimer += value;
        if (ProductionTimer >= ProductionTime)
        {
            ProductionTimer = 0;
            OutputStorage = new Resource(Output.Item, OutputStorage.Quantity + Output.Quantity);
        }
    }

    public virtual bool CanProduct()
    {
        return true;
    }

    public Resource Collect()
    {
        if (OutputStorage.Quantity == 0)
        {
            throw new Exception("Can't collect resource");
        }

        Resource resource = new Resource(OutputStorage);
        OutputStorage = new Resource(Output.Item, 0);
        return resource;
    }
}
