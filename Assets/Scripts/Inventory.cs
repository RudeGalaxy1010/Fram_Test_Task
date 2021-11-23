using System;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public class Inventory : ISaveAgent<List<Resource>>
{
    public UnityAction<List<Resource>> Changed;
    public UnityAction Cleared;

    public List<Resource> Resources { get; private set; }

    public Inventory()
    {
        Resources = new List<Resource>();
    }

    public bool Has(Resource resource)
    {
        for (int i = 0; i < Resources.Count; i++)
        {
            if (Resources[i].Equals(resource) && Resources[i].Quantity >= resource.Quantity)
            {
                return true;
            }
        }

        return false;
    }

    public void Add(Resource resource)
    {
        for (int i = 0; i < Resources.Count; i++)
        {
            if (Resources[i].Equals(resource))
            {
                Resources[i] = new Resource(resource.Item, Resources[i].Quantity + resource.Quantity);
                Changed?.Invoke(Resources);
                return;
            }
        }

        Resources.Add(new Resource(resource));
        Changed?.Invoke(Resources);
    }

    public void Remove(Resource resource)
    {
        if (!Has(resource))
        {
            throw new System.Exception("Not enough resources");
        }

        for (int i = 0; i < Resources.Count; i++)
        {
            if (Resources[i].Equals(resource))
            {
                Resources[i] = new Resource(resource.Item, Resources[i].Quantity - resource.Quantity);
                Changed?.Invoke(Resources);
                return;
            }
        }
        Changed?.Invoke(Resources);
    }

    public void Clear()
    {
        Resources.Clear();
        Changed?.Invoke(Resources);
        Cleared?.Invoke();
    }

    public List<Resource> GetValues()
    {
        return Resources;
    }

    public void SetValues(List<Resource> data)
    {
        Clear();
        Resources = data;
        Changed?.Invoke(Resources);
    }
}
