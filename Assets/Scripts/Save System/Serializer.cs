using System;
using System.Collections.Generic;
using UnityEngine;
using Farm.Core;

public class Serializer
{
    private List<CellSaveData> _cropSaves;
    private InventorySaveData _inventorySaveData;

    public string Serialize(PrefabStorage prefabStorage, List<Cell> cells, Inventory inventory)
    {
        _cropSaves = new List<CellSaveData>();

        for (int i = 0; i < cells.Count; i++)
        {
            _cropSaves.Add(new CellSaveData
            {
                CellId = cells[i].Id,
                CropId = cells[i].CropId,
                Position = cells[i].transform.position,
                Crop = cells[i].Crop
            });
        }

        _inventorySaveData = new InventorySaveData { Resources = inventory.Resources };

        SaveData saveData = new SaveData
        {
            CellSaves = _cropSaves,
            InventorySaveData = _inventorySaveData
        };

        return JsonUtility.ToJson(saveData);
    }

    public SaveData Deserialize(string jsonString)
    {
        return JsonUtility.FromJson<SaveData>(jsonString);
    }

    [Serializable]
    public struct SaveData
    {
        public List<CellSaveData> CellSaves;
        public InventorySaveData InventorySaveData;
    }

    [Serializable]
    public struct CellSaveData
    {
        public int CellId;
        public int CropId;
        public Vector3 Position;
        public Crop Crop;
    }

    [Serializable]
    public struct InventorySaveData
    {
        public List<Resource> Resources;
    }
}
