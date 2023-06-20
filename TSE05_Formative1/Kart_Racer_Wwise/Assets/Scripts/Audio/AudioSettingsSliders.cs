using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class WwiseBus
{
    public Slider slider;
    public string rtpcName;

    public void AddListeners()
    {
        slider.onValueChanged.AddListener(delegate
        {
            AkSoundEngine.SetRTPCValue(rtpcName, slider.value);
            PlayerPrefs.SetFloat(rtpcName, slider.value);
        });
    }

    public void LoadVolume()
    {
        if (!PlayerPrefs.HasKey(rtpcName))
            PlayerPrefs.SetFloat(rtpcName, 0.8f);

        slider.value = PlayerPrefs.GetFloat(rtpcName);
    }
    
    public void RemoveListeners() => slider.onValueChanged.RemoveAllListeners();

    public void DeleteData() => PlayerPrefs.DeleteKey(rtpcName);
}

public class AudioSettingsSliders : MonoBehaviour
{
    public List <WwiseBus> buses;
    
    private void OnEnable()
    {
        foreach (var bus in buses)
        {
            bus.LoadVolume();
            bus.AddListeners();
        }
    }

    private void OnDisable()
    {
        foreach (var bus in buses)
        {
            bus.RemoveListeners();
        }
    }

    public void DeleteUserData()
    {
        foreach (var bus in buses)
        {
            bus.DeleteData();
            bus.LoadVolume();
        }
    }
}
