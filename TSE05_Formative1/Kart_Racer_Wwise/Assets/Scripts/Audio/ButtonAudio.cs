using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Audio
{
    public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button _button;
        public string hoverEvent, selectEvent;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            AkSoundEngine.SetRTPCValue("ButtonHover", 1f, gameObject);
            AkSoundEngine.PostEvent(hoverEvent, gameObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            AkSoundEngine.SetRTPCValue("ButtonHover", 0f, gameObject);
            AkSoundEngine.PostEvent(hoverEvent, gameObject);
        }

        private void OnButtonClick()
        {
            AkSoundEngine.PostEvent(selectEvent, gameObject);
        }
    }
}

