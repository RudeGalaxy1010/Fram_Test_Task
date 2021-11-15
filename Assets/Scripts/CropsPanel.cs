using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Farm.Core;

namespace Farm.UI
{
    public class CropsPanel : MonoBehaviour
    {
        [SerializeField] private List<Image> Frames;

        private CropItemButton _selectedButton;
        private Inventory _inventory;
        private Field _field;

        public void Init(Field field, Inventory inventory)
        {
            Hide();
            _field = field;
            _field.CellSelected += Show;
            _field.CellDeselected += Hide;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SelectItem(CropItemButton cropItemButton)
        {
            _selectedButton = cropItemButton;
            _selectedButton.Select();
        }

        public void TryPlaceItem()
        {
            if (_selectedButton == null)
            {
                return;
            }

            //if (!_inventory.Has(_selectedCrop.Requirement))
            //{
            //    return;
            //}

            //_inventory.Remove(_selectedCrop.Requirement);
            _field.Place(_selectedButton.Crop);
            _selectedButton.Deselect();
            _selectedButton = null;
        }

        private void OnDestroy()
        {
            _field.CellSelected -= Show;
            _field.CellDeselected -= Hide;
        }
    }
}
