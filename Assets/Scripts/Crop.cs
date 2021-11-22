using System;
using UnityEngine;
using UnityEngine.Events;

namespace Farm.Core
{
    public class Crop : Productable
    {
        public Crop(float productionTimer, Resource output) : base(productionTimer, output) { }

        public override bool CanProduct()
        {
            return true;
        }

        public override object GetValues()
        {
            return this;
        }

        public override void SetValues(object data)
        {
            base.SetValues(data);
        }
    }
}