using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _quantity;

    public void Set(Resource resource)
    {
        _name.text = resource.Item.Name;
        _quantity.text = resource.Quantity.ToString();
    }
}
