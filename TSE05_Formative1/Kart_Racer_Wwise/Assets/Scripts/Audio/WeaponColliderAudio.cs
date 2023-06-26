using UnityEngine;

namespace Audio
{
    public class WeaponColliderAudio : MonoBehaviour
    {
        public string collisionEvent = "Play_Collision";
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 8)
                AkSoundEngine.PostEvent(collisionEvent, collision.gameObject);
        }
    }
}

