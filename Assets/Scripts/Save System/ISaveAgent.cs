using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveAgent<T>
{
    public T GetValues();
    public void SetValues(T data);
}
