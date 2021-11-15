using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Farm.Core
{
    [Serializable]
    public class Crop
    {
        public UnityAction<float> Updated;

        public float GrowTime { get; private set; }
        public float GrowTimer { get; private set; }
        public Resource Output { get; private set; }

        public float Progress => Mathf.Min(GrowTimer / GrowTime, 1f);

        public void Init(float growTime, Resource output)
        {
            GrowTime = growTime;
            GrowTimer = 0;
            Output = new Resource(output);
        }

        public void AddProgress(float value)
        {
            if (GrowTimer >= GrowTime)
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
                throw new System.Exception("Can't collect resource. Crop still growing");
            }

            GrowTimer = 0;
            return Output;
        }
    }
}