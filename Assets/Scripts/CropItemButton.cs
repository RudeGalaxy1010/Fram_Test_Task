using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CropItemButton : MonoBehaviour
{
    [SerializeField] private Image _frame;

    private CropSettings _cropSettings;

    public CropSettings Crop => _cropSettings;

    public void Init(CropSettings cropSettings)
    {
        _cropSettings = cropSettings;
    }

    public void Select()
    {
        _frame.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _frame.gameObject.SetActive(false);
    }
}
