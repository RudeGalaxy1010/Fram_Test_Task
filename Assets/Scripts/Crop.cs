using System;
using UnityEngine;
using UnityEngine.Events;

namespace Farm.Core
{
    [Serializable]
    public class Crop
    {
        public UnityAction<float> Updated;

        public int SettingsId { get; private set; } = -1;
        public float GrowTime { get; private set; }
        public float GrowTimer { get; private set; }
        public Resource Output { get; private set; }

        public float Progress => Mathf.Min(GrowTimer / GrowTime, 1f);

        public void Init(CropSettings settings)
        {
            SettingsId = settings.Id;
            GrowTime = settings.GrowTime;
            GrowTimer = 0;
            Output = new Resource(settings.Output);
        }

        public void AddProgress(float value)
        {
            if (Progress == 1)
            {
                return;
            }

            GrowTimer += value;
            Updated?.Invoke(Progress);
        }

        public Resource Collect()
        {
            if (Progress != 1)
            {
                throw new Exception("Can't collect resource");
            }

            GrowTimer = 0;
            return Output;
        }
    }
}