using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class WwiseBus
{
    public Slider _slider;
    public string _rtpcName;
    
    public void AddListener()
    {
        _slider.onValueChanged.AddListener(delegate
        {
            AkSoundEngine.SetRTPCValue(_rtpcName, _slider.value);
            SaveVolume(_slider.value);
        });
    }
    
    public void LoadVolume()
    {
        if (!PlayerPrefs.HasKey(_rtpcName))
            PlayerPrefs.SetFloat(_rtpcName, 0.8f);

        _slider.value = PlayerPrefs.GetFloat(_rtpcName);
    }
    
    public void RemoveListener()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }
    
    private void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat(_rtpcName, value);
    }
    
    public void DeleteData()
    {
        PlayerPrefs.DeleteKey(_rtpcName);
    }
}

public class AudioSettingsSliders : MonoBehaviour
{
    public List <WwiseBus> buses;
        
    //when this object is enabled, it loads the volumes and adds the listener
    private void OnEnable()
    {
        foreach (var bus in buses)
        {
            bus.LoadVolume();
            bus.AddListener();
        }
    }
    //when this object it disabled, it removes the listener
    private void OnDisable()
    {
        foreach (var bus in buses)
        {
            bus.RemoveListener();
        }
    }
    //and finally a method we can call to remove all the user data!
    public void DeleteUserData()
    {
        foreach (var bus in buses)
        {
            bus.DeleteData();
            bus.LoadVolume();
        }
    }
}
