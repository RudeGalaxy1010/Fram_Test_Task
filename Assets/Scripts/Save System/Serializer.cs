using System;
using System.Collections.Generic;
using UnityEngine;
using Farm.Core;

public class Serializer
{
    private List<CropSaveData> _cropSaves;
    private InventorySaveData _inventorySaveData;

    public void Serialize(Field field, Inventory inventory)
    {
        _cropSaves = new List<CropSaveData>();

        foreach (var crop in field.Crops)
        {
            int cellId = crop.Key;
            Crop cropData = crop.Value;

            _cropSaves.Add(new CropSaveData
            {
                CellId = cellId,
                GrowTime = cropData.GrowTime,
                GrowTimer = cropData.GrowTimer,
                Output = cropData.Output
            });
        }

        _inventorySaveData = new InventorySaveData { Resources = inventory.Resources };

        SaveData saveData = new SaveData
        {
            CropSaves = _cropSaves,
            InventorySaveData = _inventorySaveData
        };

        string result = JsonUtility.ToJson(saveData);
        Debug.Log(result);
    }

    [Serializable]
    public struct SaveData
    {
        public List<CropSaveData> CropSaves;
        public InventorySaveData InventorySaveData;
    }

    [Serializable]
    public struct CropSaveData
    {
        public int CellId;
        public float GrowTime;
        public float GrowTimer;
        public Resource Output;
    }

    [Serializable]
    public struct InventorySaveData
    {
        public List<Resource> Resources;
    }
}
