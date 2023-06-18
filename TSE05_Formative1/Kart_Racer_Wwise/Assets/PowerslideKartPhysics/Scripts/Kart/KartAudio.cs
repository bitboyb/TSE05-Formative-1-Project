// Copyright (c) 2020 Justin Couch / JustInvoke
using UnityEngine;
using System.Collections;

namespace PowerslideKartPhysics
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Kart))]
    //Class for playing kart sounds
    public class KartAudio : MonoBehaviour
    {
        Kart theKart;
        public bool zeroDoppler = true;
     

        private AK.Wwise.RTPC speedRTPC;
        private AK.Wwise.Switch groundSurface;

        private float kartSpeed;
        private bool isBoosting;
        private bool isDrifting;

        //Wwise events
        public AK.Wwise.Event engineSnd;
        public AK.Wwise.Event jumpSnd;
        public AK.Wwise.Event landSnd;
        public AK.Wwise.Event collisionSnd;
        public AK.Wwise.Event boostStartSnd;
        public AK.Wwise.Event boostStopSnd;
        public AK.Wwise.Event boostFailSnd;
        public AK.Wwise.Event tireSnd;
        public AK.Wwise.Event stopTireSnd;
        public AK.Wwise.Event itemUseSnd;
        public AK.Wwise.Event itemHitSnd;

        private void Awake()
        {
            theKart = GetComponent<Kart>();

            //Post Wwise event
            engineSnd.Post(gameObject);

            isDrifting = false;

        }

        private void Update()
        {
            if (theKart == null) { return; }

            //Get Kart velocity variable
            kartSpeed = theKart.localVel.z;

            AkSoundEngine.SetRTPCValue("Speed", kartSpeed);
            //Debug.Log(theKart.drifting);

            if (theKart.boostReserve < 0.1 && isBoosting == true)
            {
                PlayBoostStopSnd();
            }

            //check if drifting and run skidding audio on correct surface
            DriftingCheck();

        }

        //Play the jump sound
        public void PlayJumpSnd()
        {
            jumpSnd.Post(gameObject);
        }

        //Play the land sound
        public void PlayLandSnd()
        {
            landSnd.Post(gameObject);
        }

        //Wrapper for the collision sound playing function
        //The position vector is a dummy parameter for the sake of working with the collision UnityEvent in the Kart class
        public void PlayCollisionSnd(Vector3 pos, Vector3 vel)
        {
            PlayCollisionSnd(vel.magnitude);
        }

        //Play a random collision sound with the given volume factor
        public void PlayCollisionSnd(float volume)
        {
            collisionSnd.Post(gameObject);
        }

        //Play the boost start sound
        public void PlayBoostStartSnd()
        {

            if (isBoosting == false)
            {
                boostStartSnd.Post(gameObject);

                isBoosting = true;
            }
            
        }

        public void PlayBoostStopSnd()
        {
            boostStopSnd.Post(gameObject);

            isBoosting = false;
        }

        //Play the boost fail sound
        public void PlayBoostFailSnd()
        {
            boostFailSnd.Post(gameObject);
        }

        //Play the sound for using and item
        public void PlayItemUseSnd()
        {
            itemUseSnd.Post(gameObject);
        }

        //Play the sound for getting hit by an item
        public void PlayItemHitSnd()
        {
            itemHitSnd.Post(gameObject);
        }

        public void SetItemSwitch(string itemName)
        {
            AkSoundEngine.SetSwitch("ItemType", itemName, gameObject);
        }
        
        public void DriftingCheck()
        {
            GroundSurfacePreset currentsurface = theKart.GetWheelSurface();

            //only run if drifting
            if (theKart.drifting == true)
            {
                //get current switch state and store in switchState variable
                uint switchState;

                AkSoundEngine.GetSwitch("GroundSurface", gameObject, out switchState);

                //only change switch if different from current switch state
                if (currentsurface == true && switchState != 1481171086)
                {
                    AkSoundEngine.SetSwitch("GroundSurface", "OffRoad", gameObject);
                }
                else if (currentsurface == false && switchState != 3769881715)
                {
                    AkSoundEngine.SetSwitch("GroundSurface", "Tarmac", gameObject);
                }
            }

            if (theKart.drifting == true && theKart.grounded == true && isDrifting == false)
            {
                tireSnd.Post(gameObject);

                isDrifting = true;
            }

            if (theKart.grounded == false ^ theKart.drifting == false && isDrifting == true)
            {
                stopTireSnd.Post(gameObject);

                isDrifting = false;
            }
        }
    }
}