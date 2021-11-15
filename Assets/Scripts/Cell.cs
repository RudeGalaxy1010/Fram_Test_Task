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
        public int CropId { get; private set; }

        private GameObject _view { get; set; }

        public bool IsFree => Crop == null;

        public void Init(int id)
        {
            Id = id;
            CropId = -1;
        }

        public void Init(int id, Crop crop, CropSettings settings)
        {
            Id = id;
            Crop = crop;
            CropId = settings.Id;
            _view = Instantiate(settings.Prefab, transform.position + _itemsOffset, Quaternion.identity, transform);
        }

        public void SummonCrop(CropSettings settings)
        {
            if (Crop != null)
            {
                throw new System.Exception("Cell is not free");
            }

            CropId = settings.Id;
            Crop = new Crop();
            Crop.Init(settings.GrowTime, settings.Output);
            _view = Instantiate(settings.Prefab, transform.position + _itemsOffset, Quaternion.identity, transform);
        }

        public void OnClick()
        {
            Clicked?.Invoke(this);
        }

        public void Clear()
        {
            Crop = null;
            Destroy(_view);
        }

        public void Tick(float value)
        {
            if (Crop == null)
            {
                return;
            }

            Crop.AddProgress(value);
        }
    }
}
