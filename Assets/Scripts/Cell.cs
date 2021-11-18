using UnityEngine;
using UnityEngine.Events;

namespace Farm.Core
{
    [RequireComponent(typeof(Collider))]
    public class Cell: MonoBehaviour, IUpdateable
    {
        [SerializeField] private Vector3 _itemsOffset;

        public UnityAction<Cell> Clicked;

        public int Id { get; private set; }
        public Crop Crop { get; private set; }
        public Animal Animal { get; private set; }

        private GameObject _view { get; set; }

        public bool IsFree => Crop == null && Animal == null;

        public void Init(int id)
        {
            Id = id;
            Crop = null;
        }

        public void SummonCrop(Crop crop, GameObject prefab)
        {
            if (!IsFree)
            {
                throw new System.Exception("Cell is not free");
            }

            Crop = crop;
            _view = Instantiate(prefab, transform.position + _itemsOffset, Quaternion.identity, transform);
        }

        public void SummonAnimal(Animal animal, GameObject prefab)
        {
            if (!IsFree)
            {
                throw new System.Exception("Cell is not free");
            }

            Animal = animal;
            _view = Instantiate(prefab, transform.position + _itemsOffset, Quaternion.identity, transform);
        }

        public void OnClick()
        {
            Clicked?.Invoke(this);
        }

        public void Clear()
        {
            Crop = null;
            Animal = null;
            Destroy(_view);
        }

        public void Tick(float value)
        {
            if (Crop != null)
            {
                Crop.AddProgress(value);
            }

            if (Animal != null)
            {
                Animal.AddProgress(value);
            }
        }
    }
}
