using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farm.UI;

namespace Farm.Core
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private FieldGenerator _fieldGenerator;
        [SerializeField] private PrefabStorage _storage;
        [SerializeField] private Field _field;
        [SerializeField] private CropsPanel _cropsPanel;
        [SerializeField] private InventoryPanel _inventoryDisplay;
        [SerializeField] private Targets _targets;
        [SerializeField] private TargetsPanel _targetsPanel;
        [SerializeField] private LevelManager _levelManager;

        private List<Cell> _cells;
        private Inventory _inventory;

        private void Start()
        {
            _inventory = new Inventory();
            _inventoryDisplay.Bind(_inventory);

            if (PlayerPrefsSaveSystem.HasSave)
            {
                LoadAll();
            }

            _targets.Complete += () =>
            {
                _levelManager.SwitchNextLevel();
                ClearLevel();
                GenerateLevel();
            };

            GenerateLevel();
            _cropsPanel.Init(_field);
        }

        public void ClearLevel()
        {
            _inventory.Clear();
            if (_cells == null)
            {
                _cells = new List<Cell>();
            }

            for (int i = 0; i < _cells.Count; i++)
            {
                Destroy(_cells[i].gameObject);
            }
            _cells.Clear();
        }

        public void GenerateLevel()
        {
            _cells = _fieldGenerator.CreateField(_levelManager.LevelData.FieldSize.x, _levelManager.LevelData.FieldSize.y);
            _field.Init(_cells, _inventory);
            _targets.Init(_inventory, _levelManager.LevelData.Targets.Resources);
            _targetsPanel.Init(_levelManager.LevelData.Targets);
            _inventoryDisplay.Bind(_inventory);
        }

        public void SaveAll()
        {
            SaveData data = new SaveData();
            data.Inventory = _inventory.GetValues();
            data.LevelIndex = _levelManager.GetValues();
            PlayerPrefsSaveSystem.Save(data);
        }

        public void LoadAll()
        {
            SaveData data = PlayerPrefsSaveSystem.Load<SaveData>();
            _inventory.SetValues(data.Inventory);
            _levelManager.SetValues(data.LevelIndex);
        }

        private void OnApplicationQuit()
        {
            SaveAll();
        }

        private void OnDestroy()
        {
            if (_targets != null)
            {
                _targets.Complete -= () =>
                {
                    _levelManager.SwitchNextLevel();
                    GenerateLevel();
                };
            }
        }
    }
}
