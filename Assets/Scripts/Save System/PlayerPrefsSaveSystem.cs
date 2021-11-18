using UnityEngine;

public class PlayerPrefsSaveSystem
{
    private const string SAVE_KEY = "FarmGameSave";
    private JsonSerializer _serializer;

    public PlayerPrefsSaveSystem(JsonSerializer serializer)
    {
        _serializer = serializer;
    }

    public bool HasSave => PlayerPrefs.HasKey(SAVE_KEY);

    public SaveData Load()
    {
        return _serializer.Deserialize(PlayerPrefs.GetString(SAVE_KEY));
    }

    public void Save(SaveData data)
    {
        string dataString = _serializer.Serialize(data);
        PlayerPrefs.SetString(SAVE_KEY, dataString);
    }
}
