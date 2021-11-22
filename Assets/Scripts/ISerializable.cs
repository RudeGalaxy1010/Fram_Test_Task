using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializable
{
    public string GetData();
    public void SetData(string data);
}
