using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Farm.Core
{
    public class Field : MonoBehaviour
    {
        public UnityAction CellSelected;
        public UnityAction CellDeselected;

        [SerializeField] private PrefabsStorage _prefabsStorage;
        private List<Cell> _cells;
        private Cell _selectedCell;
        public Dictionary<int, Crop> Crops { get; private set; }
        private Inventory _inventory;

        public void Init(List<Cell> cells, Inventory inventory)
        {
            _inventory = inventory;
            _cells = cells;
            for (int i = 0; i < _cells.Count; i++)
            {
                _cells[i].Clicked += CheckCell;
            }
            Crops = new Dictionary<int, Crop>();
        }

        private void Update()
        {
            foreach (var crop in Crops.Values)
            {
                (crop as IUpdateable).Tick(Time.deltaTime);
            }
        }

        public void CheckCell(Cell cell)
        {
            if (Crops.ContainsKey(cell.Id))
            {
                if (Crops[cell.Id].Progress == 1)
                {
                    _inventory.Add(Crops[cell.Id].Collect());
                }

                return;
            }

            if (_selectedCell == cell)
            {
                DeselectCell();
            }
            else
            {
                SelectCell(cell);
            }
        }

        public void SelectCell(Cell cell)
        {
            DeselectCell();
            _selectedCell = cell;
            _selectedCell.transform.position += Vector3.up / 2f;
            CellSelected?.Invoke();
        }

        public void DeselectCell()
        {
            if (_selectedCell == null)
            {
                return;
            }

            _selectedCell.transform.position -= Vector3.up / 2f;
            _selectedCell = null;
            CellDeselected?.Invoke();
        }

        public void Place(CropSettings cropSettings)
        {
            // TODO:
            _inventory.Remove(cropSettings.Requirement);
            if (_selectedCell == null)
            {
                throw new System.Exception("Cell is not selected");
            }
            Crop crop = new Crop();
            crop.Init(cropSettings.GrowTime, cropSettings.Output);
            Crops.Add(_selectedCell.Id, crop);

            // Check if free
            GameObject newObj = Instantiate(cropSettings.Prefab);
            _selectedCell.Place(newObj);
            DeselectCell();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                _cells[i].Clicked -= CheckCell;
            }
        }
    }
}
