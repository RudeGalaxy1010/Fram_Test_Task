using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farm.UI;

namespace Farm.Core
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private FieldGenerator _fieldGenerator;
        [SerializeField] private PrefabStorage _prefabStorage;
        [SerializeField] private Field _field;
        [SerializeField] private CropsPanel _cropsPanel;

        private Inventory _inventory;
        private Serializer _serializer;

        private void Start()
        {
            //PlayerPrefs.DeleteAll();
            _serializer = new Serializer();
            _inventory = new Inventory();
            List<Cell> cells = new List<Cell>();

            if (PlayerPrefs.HasKey("Test_Save"))
            {
                var data = _serializer.Deserialize(PlayerPrefs.GetString("Test_Save"));
                _inventory.Init(data.InventorySaveData.Resources);
                cells = _fieldGenerator.CreateCells(data.CellSaves.Count);
                for (int i = 0; i < cells.Count; i++)
                {
                    cells[i].transform.position = data.CellSaves[i].Position;
                    if (data.CellSaves[i].CropId < 0)
                    {
                        cells[i].Init(data.CellSaves[i].CellId);
                    }
                    else
                    {
                        cells[i].Init(data.CellSaves[i].CellId, data.CellSaves[i].Crop, _prefabStorage.Crops[data.CellSaves[i].CropId]);
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
                string saveData = _serializer.Serialize(_prefabStorage, _field.Cells, _inventory);
                PlayerPrefs.SetString("Test_Save", saveData);
            }
        }
    }
}
