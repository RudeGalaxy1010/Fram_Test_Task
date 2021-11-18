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
        [SerializeField] private CropItemButton _cropButtonPrefab;
        [SerializeField] private AnimalItemButton _animalButtonPrefab;

        private List<ItemButton> _buttons;
        private ItemButton _selectedButton;
        private Field _field;

        public void Init(Field field)
        {
            Hide();
            _field = field;
            _field.CellSelected += Show;
            _field.CellDeselected += Hide;

            _buttons = new List<ItemButton>();

            for (int i = 0; i < _prefabStorage.Crops.Count; i++)
            {
                CropItemButton button = Instantiate(_cropButtonPrefab, _buttonsHandler.transform.position, 
                    Quaternion.identity, _buttonsHandler);
                button.Init(_prefabStorage.Crops[i]);
                button.GetComponent<Button>().onClick.AddListener(() => SelectItem(button));
                _buttons.Add(button);
            }

            for (int i = 0; i < _prefabStorage.Animals.Count; i++)
            {
                AnimalItemButton button = Instantiate(_animalButtonPrefab, _buttonsHandler.transform.position,
                                    Quaternion.identity, _buttonsHandler);
                button.Init(_prefabStorage.Animals[i]);
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

        public void SelectItem(ItemButton cropItemButton)
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

            CropItemButton cropButton = _selectedButton as CropItemButton;
            if (cropButton != null)
            {
                _field.TrySummonCrop(cropButton.Crop);
            }

            AnimalItemButton animalButton = _selectedButton as AnimalItemButton;
            if (animalButton != null)
            {
                _field.TrySummonAnimal(animalButton.Animal);
            }

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
