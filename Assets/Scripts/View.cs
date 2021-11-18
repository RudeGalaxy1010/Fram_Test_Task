using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private Image _progress;
    [SerializeField] private Animation _animation;

    private Productable _productable;

    public void Bind(Productable productable)
    {
        _productable = productable;
        _productable.Updated += (p) => _progress.fillAmount = p;
    }

    private void Update()
    {
        if (!_productable.CanProduct() && !_animation.isPlaying)
        {
            _animation.Play();
        }
        else if (_productable.CanProduct() && _animation.isPlaying)
        {
            _animation.Stop();
        }
    }

    private void OnDestroy()
    {
        _productable.Updated -= (p) => _progress.fillAmount = p;
    }
}
