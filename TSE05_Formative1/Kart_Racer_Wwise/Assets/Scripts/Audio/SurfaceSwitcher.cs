using System;
using UnityEngine;

namespace Audio
{
    public class SurfaceSwitcher : MonoBehaviour
    {
        private float _raycastDistance = 2f;
        private bool _isPlayingSurfaceEvent;

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, _raycastDistance))
            {
                if (!_isPlayingSurfaceEvent)
                {
                    string objectTag = hit.collider.gameObject.tag;
                    
                    if (objectTag == null || objectTag == "")
                        objectTag = "Track";
                    
                    SetSurfaceSwitch(objectTag);
                    _isPlayingSurfaceEvent = true;
                }
            }
            else if (_isPlayingSurfaceEvent)
            {
                _isPlayingSurfaceEvent = false;
                StopSurfaceEvent();
            }
        }

        private void SetSurfaceSwitch(string objectTag)
        {
            AkSoundEngine.SetSwitch("GroundSurface", objectTag, gameObject);
            AkSoundEngine.PostEvent("Play_Surface", gameObject);
        }
        
        private void StopSurfaceEvent()
        {
            AkSoundEngine.PostEvent("Stop_Surface", gameObject);
        }
    }
}