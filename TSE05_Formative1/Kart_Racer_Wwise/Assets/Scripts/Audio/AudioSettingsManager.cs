using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioSettingsManager : MonoBehaviour
    {
        public string[] playerPrefsNames;
        private void Start()
        {
            LoadPlayerPrefs();
        }
        
        private void LoadPlayerPrefs()
        {
            foreach (var pref in playerPrefsNames)
            {
                if(PlayerPrefs.HasKey(pref))
                    AkSoundEngine.SetRTPCValue(pref, PlayerPrefs.GetFloat(pref));
                else
                {
                    PlayerPrefs.SetFloat(pref, 0.8f);
                    AkSoundEngine.SetRTPCValue(pref, 0.8f);
                }
            }
        }
    }
}

