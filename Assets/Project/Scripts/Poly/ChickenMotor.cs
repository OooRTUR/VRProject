using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Poly
{
    public class ChickenMotor : AnimalMotor
    {
        protected override void Awake()
        {
            base.Awake();
            Debug.Log(base.animalType.type);
        }
    }
}
