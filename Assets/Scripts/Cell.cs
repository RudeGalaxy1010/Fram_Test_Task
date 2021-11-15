using UnityEngine;
using UnityEngine.Events;

namespace Farm.Core
{
    [RequireComponent(typeof(Collider))]
    public class Cell: MonoBehaviour
    {
        [SerializeField] private Vector3 _itemsOffset;

        public UnityAction<Cell> Clicked;

        public int Id { get; private set; }

        private GameObject _item { get; set; }
        private bool _isFree { get; set; }

        public void Init(int id)
        {
            Id = id;
        }

        public void Place(GameObject item)
        {
            _item = item;
            _item.transform.position = transform.position + _itemsOffset;
            _isFree = false;
        }

        public void OnClick()
        {
            Clicked?.Invoke(this);
        }

        public void Clear()
        {
            Destroy(_item);
            _isFree = true;
        }
    }
}
