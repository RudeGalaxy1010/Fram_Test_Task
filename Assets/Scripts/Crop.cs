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
    }
}