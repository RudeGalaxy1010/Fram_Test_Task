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
                if (cell.Productable.OutputStorage.Quantity > 0)
                {
                    _inventory.Add(cell.Productable.Collect());
                }
                else
                {
                    Animal animal = cell.Productable as Animal;
                    if (animal != null && _inventory.Has(animal.Input.Resource))
                    {
                        _inventory.Remove(animal.Input.Resource);
                        animal.AddInput();
                    }
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
            if (_selectedCell == null || !_selectedCell.IsFree)
            {
                return;
            }

            Crop crop = new Crop(cropSettings.GrowTime, cropSettings.Output);
            _selectedCell.SummonProductable(crop, cropSettings.Prefab);

            DeselectCell();
        }

        public void TrySummonAnimal(AnimalSettings animalSettings)
        {
            if (_selectedCell == null || !_selectedCell.IsFree)
            {
                return;
            }

            Animal animal = new Animal(animalSettings.ProductionTime, animalSettings.Input, animalSettings.Output);
            _selectedCell.SummonProductable(animal, animalSettings.Prefab);

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
