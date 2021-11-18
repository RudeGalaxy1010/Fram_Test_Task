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
            _serializer = new JsonSerializer();
            _saveSystem = new PlayerPrefsSaveSystem(_serializer);
            _inventory = new Inventory();
            List<Cell> cells = new List<Cell>();

            _inventory.Init(new List<Resource>());
            cells = _fieldGenerator.CreateField(10, 10);

            _field.Init(cells, _inventory);
            _cropsPanel.Init(_field);
        }
    }
}
