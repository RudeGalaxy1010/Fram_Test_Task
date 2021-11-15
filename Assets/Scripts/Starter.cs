using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farm.UI;

namespace Farm.Core
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private FieldGenerator _fieldGenerator;
        [SerializeField] private Field _field;
        [SerializeField] private CropsPanel _cropsPanel;

        private Inventory _inventory;
        private Serializer _serializer;

        private void Start()
        {
            _inventory = new Inventory();
            _serializer = new Serializer();
            _inventory.Init(new List<Resource>());
            var cells = _fieldGenerator.CreateField(10, 10);
            _field.Init(cells, _inventory);
            _cropsPanel.Init(_field, _inventory);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _serializer.Serialize(_field, _inventory);
            }
        }
    }
}
