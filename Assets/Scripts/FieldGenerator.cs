using System.Collections.Generic;
using UnityEngine;

namespace Farm.Core
{
    public class FieldGenerator : MonoBehaviour
    {
        [SerializeField] private Cell _cellPrefab;

        public List<Cell> CreateField(int x, int y)
        {
            List<Cell> result = new List<Cell>();

            for (int i = 1; i <= x; i++)
            {
                for (int j = 1; j <= y; j++)
                {
                    Vector3 position = new Vector3(i - x/2f, 0, j - y/2f);
                    Cell cell = Instantiate(_cellPrefab, position, Quaternion.identity, transform);
                    cell.Init(i + j);
                    result.Add(cell);
                }
            }

            return result;
        }

        public List<Cell> CreateCells(int cellsCount)
        {
            List<Cell> result = new List<Cell>();

            for (int i = 1; i <= cellsCount; i++)
            {
                Cell cell = Instantiate(_cellPrefab, Vector3.zero, Quaternion.identity, transform);
                result.Add(cell);
            }

            return result;
        }
    }
}
