using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class KartTypeSetter : MonoBehaviour
    {
        public string vehicleTypeName;

        private void Start()
        {
            AkSoundEngine.SetSwitch("VehicleType", vehicleTypeName, gameObject);
        }
    }
}

