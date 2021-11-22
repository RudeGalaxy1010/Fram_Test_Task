using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private ItemView _itemPrefab;
    [SerializeField] private Transform _itemsHandler;

    private List<ItemView> _items;
    private Inventory _inventory;

    public void Bind(Inventory inventory)
    {
        _items = new List<ItemView>();
        _inventory = inventory;
        _inventory.Changed += UpdateItems;
        _inventory.Cleared += ClearItems;
        ClearItems();
        CreateItems(_inventory.Resources.Count);
        UpdateItems(_inventory.Resources);
    }

    public void UpdateItems(List<Resource> resources)
    {
        if (_items.Count < resources.Count)
        {
            CreateItems(resources.Count - _items.Count);
        }
        else if (_items.Count > resources.Count)
        {
            ClearItems();
            UpdateItems(resources);
        }

        for (int i = 0; i < resources.Count; i++)
        {
            _items[i].Set(resources[i]);
        }
    }

    private void CreateItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ItemView item = Instantiate(_itemPrefab, _itemsHandler.transform.position,
                Quaternion.identity, _itemsHandler);
            _items.Add(item);
        }
    }

    public void ClearItems()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            Destroy(_items[i].gameObject);
        }

        _items.Clear();
    }

    private void OnDestroy()
    {
        _inventory.Changed -= UpdateItems;
        _inventory.Cleared -= ClearItems;
    }
}
