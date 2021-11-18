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

        private Inventory _inventory;
        private JsonSerializer _serializer;
        private PlayerPrefsSaveSystem _saveSystem;

        private void Start()
        {
            //PlayerPrefs.DeleteAll();
            _serializer = new JsonSerializer();
            _saveSystem = new PlayerPrefsSaveSystem(_serializer);
            _inventory = new Inventory();
            List<Cell> cells = new List<Cell>();

            if (_saveSystem.HasSave)
            {
                Debug.Log("Loading");
                var data = _saveSystem.Load();
                _inventory.Init(data.InventorySaveData.Resources);
                cells = _fieldGenerator.CreateCells(data.CellSaves.Count);
                for (int i = 0; i < cells.Count; i++)
                {
                    cells[i].Init(data.CellSaves[i].CellId);
                    cells[i].transform.position = data.CellSaves[i].Position;
                    if (data.CellSaves[i].Crop.SettingsId >= 0)
                    {
                        cells[i].SummonCrop(data.CellSaves[i].Crop, _storage.Crops[data.CellSaves[i].Crop.SettingsId].Prefab);
                    }
                }
            }
            else
            {
                _inventory.Init(new List<Resource>());
                cells = _fieldGenerator.CreateField(10, 10);
            }

            _field.Init(cells, _inventory);
            _cropsPanel.Init(_field);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SaveAll();
                Debug.Log("Saving...");
            }
        }

        private void SaveAll()
        {
            InventorySaveData inventoryData = new InventorySaveData
            {
                Resources = _inventory.Resources
            };

            List<CellSaveData> cellsData = new List<CellSaveData>();

            for (int i = 0; i < _field.Cells.Count; i++)
            {
                CellSaveData cellData = new CellSaveData
                {
                    CellId = _field.Cells[i].Id,
                    Position = _field.Cells[i].transform.position,
                    Crop = _field.Cells[i].Crop
            };
                cellsData.Add(cellData);
            }

            SaveData data = new SaveData
            {
                InventorySaveData = inventoryData,
                CellSaves = cellsData
            };

            _saveSystem.Save(data);
        }
    }
}
