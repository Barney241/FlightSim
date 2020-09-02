using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlightSim
{
    public class IP_Airplane_Audio : MonoBehaviour 
    {
        #region Variables
        [Header("Airplane Audio Properties")]
        public IP_BaseAirplane_Input  input;
        public AudioSource idleSource;
        public AudioSource fullThrottleSource;
        public float maxPitchValue = 1.2f;

        private float finalVolumeValue;
        private float finalPitchValue;

        private bool isShutOff = false;
        #endregion


        public bool ReduceAudio
        {
            set { isShutOff = value; }
        }
        #region Builtin Methods
        // Use this for initialization
        void Start () 
        {
            if(fullThrottleSource)
            {
                fullThrottleSource.volume = 0f;
            }
    	}
    	
    	// Update is called once per frame
    	void Update () 
        {
            if(input)
            {
                HandleAudio();
            }
    	}
        #endregion




        #region Custom Methods
        protected virtual void HandleAudio()
        {
            if (!isShutOff)
            {
                finalVolumeValue = Mathf.Lerp(0f, 1f, input.StickyThrottle);
                finalPitchValue = Mathf.Lerp(1f, maxPitchValue, input.StickyThrottle);
            }
            else
            {
                finalPitchValue -= 0.05f;
                finalVolumeValue -= 0.00001f;
                finalPitchValue = Mathf.Lerp(0f, maxPitchValue, finalPitchValue);
                finalVolumeValue = Mathf.Lerp(0f, 1f, finalVolumeValue);

                idleSource.volume -= 0.005f;
                
            }

            if(fullThrottleSource)
            {
                fullThrottleSource.volume = finalVolumeValue;
                fullThrottleSource.pitch = finalPitchValue;
            }
        }
        #endregion
    }
}
