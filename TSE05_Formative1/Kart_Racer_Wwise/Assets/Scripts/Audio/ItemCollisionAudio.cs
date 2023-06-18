using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class ItemCollisionAudio : MonoBehaviour
    {
        public string itemColEventString;

        private void OnTriggerEnter(Collider other)
        {
            AkSoundEngine.PostEvent(itemColEventString, gameObject);
        }
    }
}

