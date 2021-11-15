using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Resource> Resources { get; private set; }

    public void Init(List<Resource> resources)
    {
        Resources = resources;
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
                return;
            }
        }

        Resources.Add(new Resource(resource));
    }

    public void Remove(Resource resource)
    {
        // TODO:
        for (int i = 0; i < Resources.Count; i++)
        {
            if (Resources[i].Equals(resource))
            {
                Resources[i] = new Resource(resource.Item, Resources[i].Quantity - resource.Quantity);
                return;
            }
        }
    }
}
