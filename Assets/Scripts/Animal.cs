using UnityEngine;
using System;

[Serializable]
public class Animal : Productable
{
    public Resource InputStorage { get; private set; }
    public ConsumableResource Input { get; set; }

    private float _consumingTimer;

    public Animal(float productionTimer, ConsumableResource input, Resource output) : base(productionTimer, output)
    {
        Input = input;
        InputStorage = new Resource(Input.Resource.Item, 0);
        _consumingTimer = 0;
    }

    public void AddInput()
    {
        InputStorage = new Resource(Input.Resource.Item, InputStorage.Quantity + Input.Resource.Quantity);
    }

    protected override void AddProgress(float value)
    {
        base.AddProgress(value);

        if (!CanProduct())
        {
            return;
        }

        _consumingTimer += value;

        if (_consumingTimer > Input.ConsumingTime)
        {
            _consumingTimer = 0;
            InputStorage = new Resource(Input.Resource.Item, InputStorage.Quantity - Input.Resource.Quantity);
        }
    }

    public override bool CanProduct()
    {
        if (InputStorage.Quantity < Input.Resource.Quantity)
        {
            return false;
        }

        return true;
    }

    public override object GetValues()
    {
        return this;
    }

    public override void SetValues(object data)
    {
        base.SetValues(data);

        Input = ((Animal)data).Input;
        InputStorage = ((Animal)data).InputStorage;
        _consumingTimer = ((Animal)data)._consumingTimer;
    }
}
