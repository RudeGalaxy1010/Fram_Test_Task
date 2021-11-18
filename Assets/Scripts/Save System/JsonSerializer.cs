using UnityEngine;

public class JsonSerializer
{
    public string Serialize(SaveData data)
    {
        Debug.Log(JsonUtility.ToJson(data));
        return JsonUtility.ToJson(data);
    }

    public SaveData Deserialize(string data)
    {
        return JsonUtility.FromJson<SaveData>(data);
    }
}
