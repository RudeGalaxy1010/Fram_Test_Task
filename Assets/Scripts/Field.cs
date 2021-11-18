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

        public List<Cell> Cells { get; private set; }

        private Cell _selectedCell;
        private Inventory _inventory;

        public void Init(List<Cell> cells, Inventory inventory)
        {
            _inventory = inventory;
            Cells = cells;
            for (int i = 0; i < Cells.Count; i++)
            {
                Cells[i].Clicked += CheckCell;
            }
        }

        private void Update()
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                (Cells[i] as IUpdateable).Tick(Time.deltaTime);
            }
        }

        public void CheckCell(Cell cell)
        {
            if (!cell.IsFree)
            {
                if (cell.Crop.Progress == 1)
                {
                    _inventory.Add(cell.Crop.Collect());
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

        public void TrySummonCrop(CropSettings cropSettings)
        {
            if (_selectedCell == null)
            {
                return;
            }

            if (_selectedCell.IsFree)
            {
                Crop crop = new Crop();
                crop.Init(cropSettings);
                _selectedCell.SummonCrop(crop, cropSettings.Prefab);
            }

            DeselectCell();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                Cells[i].Clicked -= CheckCell;
            }
        }
    }
}
