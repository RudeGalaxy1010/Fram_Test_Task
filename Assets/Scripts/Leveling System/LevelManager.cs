using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour, ISaveAgent<int>
{
    [SerializeField] private List<LevelData> _levelsData;

    private int _levelIndex = 0;

    public LevelData LevelData => _levelsData[_levelIndex];

    public void SwitchNextLevel()
    {
        _levelIndex++;

        // Loop levels
        if (_levelIndex >= _levelsData.Count)
        {
            _levelIndex = 0;
        }
    }

    public int GetValues()
    {
        return _levelIndex;
    }

    public void SetValues(int data)
    {
        _levelIndex = data;
    }
}
