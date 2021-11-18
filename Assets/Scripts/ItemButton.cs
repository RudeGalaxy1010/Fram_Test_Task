using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Image _frame;

    public void Init(string name)
    {
        Deselect();
        _name.text = name;
    }

    public void Select()
    {
        _frame.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _frame.gameObject.SetActive(false);
    }
}
