using UnityEngine;

public static class PlayerPrefsSaveSystem
{
    private const string SAVE_KEY = "FarmGameSave";

    public static bool HasSave => PlayerPrefs.HasKey(SAVE_KEY);

    public static T Load<T>()
    {
        return JsonUtility.FromJson<T>(PlayerPrefs.GetString(SAVE_KEY));
    }

    public static void Save<T>(T data)
    {
        PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(data));
    }
}
