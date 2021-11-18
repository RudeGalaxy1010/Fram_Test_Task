using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnimalItemButton : ItemButton
{
    private AnimalSettings _animalSettings;

    public AnimalSettings Animal => _animalSettings;

    public void Init(AnimalSettings animalSettings)
    {
        Init(animalSettings.name);
        _animalSettings = animalSettings;
    }
}
