using Farm.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

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
    public Vector3 Position;
    public Crop Crop;
}

[Serializable]
public struct InventorySaveData
{
    public List<Resource> Resources;
}
