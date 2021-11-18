using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnimalItemButton : MonoBehaviour
{
    [SerializeField] private Image _frame;

    private AnimalSettings _animalSettings;

    public AnimalSettings Animal => _animalSettings;

    public void Init(AnimalSettings animalSettings)
    {
        _animalSettings = animalSettings;
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
