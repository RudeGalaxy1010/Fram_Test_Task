using UnityEngine;
using UnityEngine.Events;

namespace Farm.Core
{
    [RequireComponent(typeof(Collider))]
    public class Cell : MonoBehaviour, IUpdateable
    {
        [SerializeField] private Vector3 _itemsOffset;

        public UnityAction<Cell> Clicked;

        public int Id { get; private set; }
        public Productable Productable { get; private set; }

        private View _view { get; set; }
        private Vector3 _position { get; set; }

        public bool IsFree => Productable == null;

        public void Init(int id)
        {
            Id = id;
            _position = transform.position;
            Clear();
        }

        public void SummonProductable(Productable productable, View prefab)
        {
            if (!IsFree)
            {
                throw new System.Exception("Cell is not free");
            }

            Productable = productable;
            _view = Instantiate(prefab, transform.position + _itemsOffset, Quaternion.identity, transform);
            _view.Bind(Productable);
        }

        public void OnClick()
        {
            Clicked?.Invoke(this);
        }

        public void Clear()
        {
            Productable = null;
            Destroy(_view);
        }

        public void Tick(float value)
        {
            Productable?.Update(value);
        }
    }
}
