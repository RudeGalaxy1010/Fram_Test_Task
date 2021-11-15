using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Farm.Core;

namespace Farm.UI
{
    public class CropsPanel : MonoBehaviour
    {
        [SerializeField] private PrefabStorage _prefabStorage;
        [SerializeField] private Transform _buttonsHandler;
        [SerializeField] private CropItemButton _buttonPrefab;
        private List<CropItemButton> _buttons;
        private CropItemButton _selectedButton;
        private Field _field;

        public void Init(Field field)
        {
            Hide();
            _field = field;
            _field.CellSelected += Show;
            _field.CellDeselected += Hide;

            _buttons = new List<CropItemButton>();

            for (int i = 0; i < _prefabStorage.Crops.Count; i++)
            {
                CropItemButton button = Instantiate(_buttonPrefab, _buttonsHandler.transform.position, 
                    Quaternion.identity, _buttonsHandler);
                button.Init(_prefabStorage.Crops[i]);
                button.GetComponent<Button>().onClick.AddListener(() => SelectItem(button));
                _buttons.Add(button);
            }
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

            _field.TrySummonCrop(_selectedButton.Crop);
            _selectedButton.Deselect();
            _selectedButton = null;
        }

        private void OnDestroy()
        {
            _field.CellSelected -= Show;
            _field.CellDeselected -= Hide;

            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }
}
