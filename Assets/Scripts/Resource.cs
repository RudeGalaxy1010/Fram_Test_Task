using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Resource
{
    public Item Item;
    public int Quantity;

    public Resource(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public Resource(Resource resource)
    {
        Item = resource.Item;
        Quantity = resource.Quantity;
    }

    public override bool Equals(object obj)
    {
        if (obj is Resource res)
        {
            return res.Item == Item;
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
