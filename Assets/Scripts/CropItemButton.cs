using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CropItemButton : ItemButton
{
    private CropSettings _cropSettings;

    public CropSettings Crop => _cropSettings;

    public void Init(CropSettings cropSettings)
    {
        Init(cropSettings.name);
        _cropSettings = cropSettings;
    }
}
